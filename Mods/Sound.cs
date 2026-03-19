/*
 * Seralyth Menu  Mods/Sound.cs
 * A community driven mod menu for Gorilla Tag with over 1000+ mods
 *
 * Copyright (C) 2026  Seralyth Software
 * https://github.com/Seralyth/Seralyth-Menu
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using ExitGames.Client.Photon;
using GorillaLocomotion;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using Seralyth.Classes.Menu;
using Seralyth.Extensions;
using Seralyth.Managers;
using Seralyth.Menu;
using Seralyth.Patches.Menu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;
using static Seralyth.Menu.Main;
using static Seralyth.Utilities.AssetUtilities;
using static Seralyth.Utilities.FileUtilities;
using Random = UnityEngine.Random;

namespace Seralyth.Mods
{
    public static class Sound
    {
        public static bool LoopAudio = false;
        public static bool OverlapAudio = false;
        public static int BindMode;
        public static string Subdirectory = "";
        public static readonly Dictionary<string, ButtonInfo[]> CachedButtons = new Dictionary<string, ButtonInfo[]>();

        public static void LoadSoundboard(bool openCategory = true)
        {
            string key = Subdirectory ?? "";

            if (CachedButtons.TryGetValue(key, out ButtonInfo[] buttons))
            {
                Buttons.buttons[Buttons.GetCategory("Soundboard")] = buttons;

                if (openCategory)
                    Buttons.CurrentCategoryName = "Soundboard";

                return;
            }

            string path = Path.Combine(PluginInfo.BaseDirectory, "Sounds", Subdirectory.TrimStart('/'));

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            List<ButtonInfo> soundButtons = new List<ButtonInfo>();

            if (Subdirectory != "")
            {
                soundButtons.Add(new ButtonInfo
                {
                    buttonText = "Exit Subdirectory",
                    overlapText = "Exit " + Subdirectory.Split("/")[^1],
                    method = () =>
                    {
                        Subdirectory = RemoveLastDirectory(Subdirectory);
                        LoadSoundboard();
                    },
                    isTogglable = false,
                    toolTip = "Returns you back to the last folder."
                });
            }
            else
            {
                soundButtons.Add(new ButtonInfo
                {
                    buttonText = "Exit Soundboard",
                    method = () => Buttons.CurrentCategoryName = "Sound Mods",
                    isTogglable = false,
                    toolTip = "Returns you back to the sound mods."
                });
            }

            string[] folders = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            soundButtons.AddRange(
                from folder in folders
                let relativePath = Path.GetRelativePath(path, folder)
                select new ButtonInfo
                {
                    buttonText = "SoundboardFolder" + relativePath.Hash(),
                    overlapText = $"<sprite name=\"Folder\">  {relativePath}  ",
                    method = () => OpenFolder(relativePath),
                    isTogglable = false,
                    toolTip = "Opens the " + relativePath + " folder."
                });

            if (!RecorderPatch.enabled || Buttons.GetIndex("Legacy Microphone").enabled)
                NotificationManager.SendNotification($"<color=grey>[</color><color=red>WARNING</color><color=grey>]</color> You are using the legacy microphone system. Modern soundboard features will not be implemented.");

            foreach (string file in files)
            {
                string fileName = Path.GetRelativePath(path, file);
                string soundName = RemoveFileExtension(fileName).Replace("_", " ");
                string soundPath = Path.GetRelativePath(PluginInfo.BaseDirectory, file).Replace("\\", "/");

                if (RecorderPatch.enabled)
                {
                    var buttonInfo = new ButtonInfo
                    {
                        buttonText = "SoundboardSound" + soundName.Hash(),
                        overlapText = soundName,
                        toolTip = "Plays \"" + soundName + "\" through your microphone."
                    };

                    if (OverlapAudio)
                    {
                        buttonInfo.method = () => PlayAudio(soundPath);
                        buttonInfo.isTogglable = false;
                    }
                    else
                    {
                        buttonInfo.method = () => PlaySoundboardSound(soundPath, buttonInfo, LoopAudio, BindMode > 0);
                        buttonInfo.disableMethod = () => StopSoundboardSound(buttonInfo);
                    }

                    soundButtons.Add(buttonInfo);
                }
                else
                {
                    if (BindMode > 0)
                    {
                        soundButtons.Add(new ButtonInfo
                        {
                            buttonText = "SoundboardSound" + soundName.Hash(),
                            overlapText = soundName,
                            method = () => PrepareBindAudio(soundPath),
                            disableMethod = StopAllSounds,
                            toolTip = "Plays \"" + fileName + "\" through your microphone."
                        });
                    }
                    else
                    {
                        if (LoopAudio)
                        {
                            soundButtons.Add(new ButtonInfo
                            {
                                buttonText = "SoundboardSound" + soundName.Hash(),
                                overlapText = soundName,
                                enableMethod = () => PlayAudio(soundPath),
                                disableMethod = StopAllSounds,
                                toolTip = "Plays \"" + fileName + "\" through your microphone."
                            });
                        }
                        else
                        {
                            soundButtons.Add(new ButtonInfo
                            {
                                buttonText = "SoundboardSound" + soundName.Hash(),
                                overlapText = fileName,
                                method = () => PlayAudio(soundPath),
                                isTogglable = false,
                                toolTip = "Plays \"" + fileName + "\" through your microphone."
                            });
                        }
                    }
                }
            }

            soundButtons.Add(new ButtonInfo
            {
                buttonText = "Stop All Sounds",
                method = StopAllSounds,
                isTogglable = false,
                toolTip = "Stops all currently playing sounds."
            });

            soundButtons.Add(new ButtonInfo
            {
                buttonText = "Open Sound Folder",
                aliases = new[] { "Open Soundboard Folder" },
                method = OpenSoundFolder,
                isTogglable = false,
                toolTip = "Opens a folder containing all of your sounds."
            });

            soundButtons.Add(new ButtonInfo
            {
                buttonText = "Reload Sounds",
                method = () =>
                {
                    CachedButtons.Clear();
                    LoadSoundboard();
                },
                isTogglable = false,
                toolTip = "Reloads all of your sounds."
            });

            soundButtons.Add(new ButtonInfo
            {
                buttonText = "Get More Sounds",
                method = LoadSoundLibrary,
                isTogglable = false,
                toolTip = "Opens a public audio library, where you can download your own sounds."
            });

            CachedButtons[key] = soundButtons.ToArray();
            Buttons.buttons[Buttons.GetCategory("Soundboard")] = CachedButtons[key];

            if (openCategory)
                Buttons.CurrentCategoryName = "Soundboard";
        }

        public static void OpenFolder(string folder)
        {
            if (string.IsNullOrEmpty(Subdirectory))
                Subdirectory = "/" + folder;
            else
                Subdirectory = Subdirectory.TrimEnd('/') + "/" + folder;

            LoadSoundboard();
        }

        public static void LoadSoundLibrary()
        {
            string library = GetHttp($"{PluginInfo.ServerResourcePath}/Audio/Mods/Fun/Soundboard/SoundLibrary.txt");
            string[] audios = AlphabetizeNoSkip(library.Split("\n"));
            List<ButtonInfo> soundbuttons = new List<ButtonInfo> { new ButtonInfo { buttonText = "Exit Sound Library", method = () => LoadSoundboard(), isTogglable = false, toolTip = "Returns you back to the soundboard." } };
            int index = 0;
            foreach (string audio in audios)
            {
                if (audio.Length > 2)
                {
                    index++;
                    string[] Data = audio.Split(";");
                    soundbuttons.Add(new ButtonInfo { buttonText = "SoundboardDownload" + index, overlapText = Data[0], method = () => DownloadSound(Data[0], $"{PluginInfo.ServerResourcePath}/Audio/Mods/Fun/Soundboard/Sounds/{Data[1]}"), isTogglable = false, toolTip = "Downloads " + Data[0] + " to your sound library." });
                }
            }
            Buttons.buttons[Buttons.GetCategory("Sound Library")] = soundbuttons.ToArray();
            Buttons.CurrentCategoryName = "Sound Library";
        }

        public static void DownloadSound(string name, string url)
        {
            if (name.Contains(".."))
                name = name.Replace("..", "");

            if (name.Contains(":"))
                return;

            string filename = Path.Combine("Sounds", Subdirectory.TrimStart('/'), $"{name}.{GetFileExtension(url)}");
            if (File.Exists($"{PluginInfo.BaseDirectory}/{filename}"))
                File.Delete($"{PluginInfo.BaseDirectory}/{filename}");

            audioFilePool.Remove(name);

            LoadSoundFromURL(url, filename, clip =>
            {
                if (clip.length < 20f)
                    Play2DAudio(clip);
            });

            CachedButtons.Remove(Subdirectory ?? "");

            NotificationManager.SendNotification("<color=grey>[</color><color=green>SUCCESS</color><color=grey>]</color> Successfully downloaded " + name + " to the soundboard.");
        }

        public static bool AudioIsPlaying;
        public static float RecoverTime = -1f;

        private static GameObject soundboardAudioManager;

        public static bool disableLocalSoundboard;
        public static void PlayAudio(AudioClip sound, bool disableMicrophone = false)
        {
            if (!PhotonNetwork.InRoom)
            {
                if (soundboardAudioManager == null)
                {
                    soundboardAudioManager = new GameObject("2DAudioMgr");
                    AudioSource temp = soundboardAudioManager.AddComponent<AudioSource>();
                    temp.spatialBlend = 0f;
                }

                AudioSource ausrc = soundboardAudioManager.GetComponent<AudioSource>();
                ausrc.volume = 1f;
                ausrc.clip = sound;
                ausrc.loop = false;
                ausrc.Play();

                AudioIsPlaying = true;
                RecoverTime = Time.time + sound.length;

                return;
            }


            if (RecorderPatch.enabled)
                VoiceManager.Get().AudioClip(sound, disableMicrophone);
            else
            {
                NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.SourceType = Recorder.InputSourceType.AudioClip;
                NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.AudioClip = sound;
                NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.RestartRecording(true);
            }

            if (!LoopAudio)
            {
                AudioIsPlaying = true;
                RecoverTime = Time.time + sound.length + 0.4f;
            }
        }

        private static readonly Dictionary<ButtonInfo, (Guid id, AudioClip clip, bool seen)> activeSounds = new Dictionary<ButtonInfo, (Guid id, AudioClip clip, bool seen)>();

        public static void PlaySoundboardSound(object file, ButtonInfo info, bool loopAudio, bool bind)
        {
            bool[] bindings = {
                rightPrimary,
                rightSecondary,
                leftPrimary,
                leftSecondary,
                leftGrab,
                rightGrab,
                leftTrigger > 0.5f,
                rightTrigger > 0.5f,
                leftJoystickClick,
                rightJoystickClick
            };

            bool shouldPlay = true;
            if (bind && BindMode > 0)
            {
                bool bindPressed = bindings[BindMode - 1];
                shouldPlay = bindPressed && !lastBindPressed;
                lastBindPressed = bindPressed;
            }

            if (!shouldPlay)
                return;

            void Play(AudioClip clip)
            {
                if (clip == null)
                    return;

                if (!activeSounds.ContainsKey(info))
                {
                    if (RecorderPatch.enabled)
                    {
                        Guid id = VoiceManager.Get().AudioClip(clip, false);
                        activeSounds[info] = (id, clip, false);
                    }
                }

                var ids = VoiceManager.Get().AudioClips.Select(c => c.Id).ToHashSet();
                var keys = activeSounds.Keys.ToList();

                foreach (var key in keys)
                {
                    var data = activeSounds[key];
                    bool existsNow = ids.Contains(data.id);

                    if (existsNow)
                    {
                        activeSounds[key] = (data.id, data.clip, true);
                        continue;
                    }

                    if (data.seen)
                    {
                        AudioClip finishedClip = data.clip;
                        activeSounds.Remove(key);

                        if (loopAudio)
                        {
                            Guid newId = VoiceManager.Get().AudioClip(finishedClip, false);
                            activeSounds[key] = (newId, finishedClip, false);
                        }
                        else if (key.enabled)
                        {
                            Toggle(key);
                        }
                    }
                }
            }

            if (file is string filePath)
                LoadSoundFromFile(filePath, Play);
            else if (file is AudioClip audioClip)
                Play(audioClip);
        }
        public static void StopSoundboardSound(ButtonInfo info)
        {
            if (activeSounds != null)
            {
                if (activeSounds.ContainsKey(info))
                {
                    VoiceManager.Get().StopAudioClip(activeSounds[info].id);
                    activeSounds.Remove(info);
                }
            }
        }
        public static void PlayAudio(string file)
        {
            if (PhotonNetwork.InRoom)
            {
                LoadSoundFromFile(file, clip =>
                {
                    PlayAudio(clip);
                });
            }
        }

        public static void StopAllSounds() // used to be FixMicrophone
        {
            if (soundboardAudioManager != null)
                soundboardAudioManager.GetComponent<AudioSource>().Stop();

            foreach (ButtonInfo[] buttonArray in CachedButtons.Values)
            {
                foreach (ButtonInfo button in buttonArray)
                {
                    if (button != null && button.enabled)
                        button.enabled = false;
                }
            }

            if (PhotonNetwork.InRoom)
            {
                if (RecorderPatch.enabled)
                {
                    if (activeSounds != null)
                        foreach (ButtonInfo info in activeSounds.Keys.ToList())
                            info.enabled = false;

                    activeSounds.Clear();
                    VoiceManager.Get().StopAudioClips();
                    NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.DebugEchoMode = false;
                }
                else
                {
                    NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.SourceType = Recorder.InputSourceType.Microphone;
                    NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.AudioClip = null;
                    NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.RestartRecording(true);
                    NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.DebugEchoMode = false;
                }
            }

            AudioIsPlaying = false;
            RecoverTime = -1f;
        }

        public static void FixMicrophone()
        {
            if (!PhotonNetwork.InRoom)
                return;
            if (RecorderPatch.enabled)
            {
                if (activeSounds != null)
                    foreach (ButtonInfo info in activeSounds.Keys)
                        info.enabled = false;
                activeSounds.Clear();
                VoiceManager.Get().StopAudioClips();
            }
            else
            {

            }
            NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.SourceType = Recorder.InputSourceType.Microphone;
            NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.AudioClip = null;
            NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.RestartRecording(true);
            NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.DebugEchoMode = false;
        }

        private static bool lastBindPressed;
        public static void PrepareBindAudio(string file)
        {
            bool[] bindings = {
                rightPrimary,
                rightSecondary,
                leftPrimary,
                leftSecondary,
                leftGrab,
                rightGrab,
                leftTrigger > 0.5f,
                rightTrigger > 0.5f,
                leftJoystickClick,
                rightJoystickClick
            };

            bool bindPressed = bindings[BindMode - 1];
            if (bindPressed && !lastBindPressed)
            {
                if (NetworkSystem.Instance.VoiceConnection.PrimaryRecorder.SourceType == Recorder.InputSourceType.AudioClip)
                    FixMicrophone();
                else
                    PlayAudio(file);
            }
            lastBindPressed = bindPressed;
        }

        public static void OpenSoundFolder()
        {
            string filePath = GetGamePath() + $"/{PluginInfo.BaseDirectory}/Sounds";
            Process.Start(filePath);
        }

        public static void SoundBindings(bool positive = true)
        {
            string[] names = {
                "None",
                "A",
                "B",
                "X",
                "Y",
                "Left Grip",
                "Right Grip",
                "Left Trigger",
                "Right Trigger",
                "Left Joystick",
                "Right Joystick"
            };

            if (positive)
                BindMode++;
            else
                BindMode--;

            BindMode %= names.Length;
            if (BindMode < 0)
                BindMode = names.Length - 1;

            Buttons.GetIndex("Sound Bindings").overlapText = "Sound Bindings <color=grey>[</color><color=green>" + names[BindMode] + "</color><color=grey>]</color>";
        }

        public static float sendEffectDelay;
        public static void BetaPlayTag(int id, float volume)
        {
            if (!NetworkSystem.Instance.IsMasterClient)
                NotificationManager.SendNotification("<color=grey>[</color><color=red>ERROR</color><color=grey>]</color> You are not master client.");
            else
            {
                if (Time.time > sendEffectDelay)
                {
                    object[] soundSendData = { id, volume, false };
                    object[] sendEventData = { PhotonNetwork.ServerTimestamp, (byte)3, soundSendData };

                    try
                    {
                        PhotonNetwork.RaiseEvent(3, sendEventData, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendUnreliable);
                    }
                    catch { }
                    RPCProtection();

                    sendEffectDelay = Time.time + 0.2f;
                }
            }
        }

        private static float soundSpamDelay;
        public static void SoundSpam(int soundId, bool constant = false)
        {
            if (rightGrab || constant)
            {
                if (Time.time > soundSpamDelay)
                    soundSpamDelay = Time.time + 0.1f;
                else
                    return;

                if (PhotonNetwork.InRoom)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, soundId, false, 999999f);
                    RPCProtection();
                }
                else
                    VRRig.LocalRig.PlayHandTapLocal(soundId, false, 999999f);
            }
        }

        public static void JmancurlySoundSpam() =>
            SoundSpam(Random.Range(336, 338));

        public static void RandomSoundSpam() =>
            SoundSpam(Random.Range(0, GTPlayer.Instance.materialData.Count));

        public static void CrystalSoundSpam()
        {
            int[] sounds = {
                Random.Range(40,54),
                Random.Range(214,221)
            };
            SoundSpam(sounds[Random.Range(0, 1)]);
        }

        private static bool squeakToggle;
        public static void SqueakSoundSpam()
        {
            if (Time.time > soundSpamDelay)
                squeakToggle = !squeakToggle;

            SoundSpam(squeakToggle ? 75 : 76);
        }

        private static bool sirenToggle;
        public static void SirenSoundSpam()
        {
            if (Time.time > soundSpamDelay)
                sirenToggle = !sirenToggle;

            SoundSpam(sirenToggle ? 48 : 50);
        }

        public static int soundId;
        public static void DecreaseSoundID()
        {
            soundId--;
            if (soundId < 0)
                soundId = GTPlayer.Instance.materialData.Count - 1;

            Buttons.GetIndex("Custom Sound Spam").overlapText = "Custom Sound Spam <color=grey>[</color><color=green>" + soundId + "</color><color=grey>]</color>";
        }

        public static void IncreaseSoundID()
        {
            soundId++;
            soundId %= GTPlayer.Instance.materialData.Count;

            Buttons.GetIndex("Custom Sound Spam").overlapText = "Custom Sound Spam <color=grey>[</color><color=green>" + soundId + "</color><color=grey>]</color>";
        }

        public static void CustomSoundSpam() => SoundSpam(soundId);

        public static void BetaSoundSpam(int id)
        {
            if (rightGrab)
                BetaPlayTag(id, 999999f);
        }
    }
}
