/*
 * Seralyth Menu  Managers/VoiceManager.cs
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

// Written with love by kingofnetflix </3
// For anyone else snooping in this class hoping to use it, you need to make sure that your recorder source type is a Factory and that the Factory is a new instance of this class.
// You may use VoiceManager.Get()
using Photon.Voice;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Seralyth.Managers
{
    public class VoiceManager : IAudioReader<float>
    {
        private int samplingRate = 48000;
        private int outputRate = 48000;
        private float gain = 1f;
        private float clipGain = 1f;
        private float pitch = 1f;
        private float clipPitch = 1f;

        private readonly int loopLength;
        private string currentDevice;
        public AudioClip microphoneClip;
        private int lastSamplePosition;
        private float step;

        private string error;

        private float[] rawMicrophoneData;
        private float[] microphoneBuffer;
        private float resamplePointer;

        private readonly object audioClipsLock = new object();

        public sealed class Clip
        {
            public Guid Id { get; set; }
            public AudioClip Source { get; set; }
            public float[] Samples;
            public int Channels;
            public float Position;
            public float Step;
            public bool MuteMicrophone;
            public float Gain = 1f;
            public float Pitch = 1f;
        }

        private readonly List<Clip> audioClips = new List<Clip>();

        private bool muteMicrophone;

        public VoiceManager(int loopLength = 1, string device = null)
        {
            this.loopLength = Mathf.Max(1, loopLength);
            Instance ??= this;
            StartRecording(device);
        }

        /// <summary>
        /// A read-only list of AudioClips currently playing.
        /// </summary>
        public IReadOnlyList<Clip> AudioClips
        {
            get
            {
                lock (audioClipsLock)
                {
                    return audioClips.ToArray();
                }
            }
        }

        /// <summary>
        /// Gets or sets the microphone's recording status. This does not stop the pushed AudioClip from playing.
        /// </summary>
        public bool MuteMicrophone
        {
            get { return muteMicrophone; }
            set { muteMicrophone = value; }
        }

        /// <summary>
        /// Gets or sets the microphone sampling rate. Setting a value restarts the microphone.
        /// </summary>
        public int SamplingRate
        {
            get { return samplingRate; }
            set
            {
                samplingRate = Mathf.Max(8000, value);
                RestartMicrophone();
            }
        }

        /// <summary>
        /// Gets or sets the output rate used for AudioClip samples.
        /// </summary>
        public int OutputRate
        {
            get { return outputRate; }
            set
            {
                outputRate = Mathf.Max(8000, value);
                RestartMicrophone();
            }
        }

        /// <summary>
        /// Gets or sets the microphone gain multiplier.
        /// </summary>
        public float Gain
        {
            get { return gain; }
            set { gain = Mathf.Max(0f, value); }
        }

        /// <summary>
        /// Gets or sets the default AudioClip gain multiplier for the Instance.
        /// </summary>
        public float ClipGain
        {
            get { return clipGain; }
            set { clipGain = Mathf.Max(0f, value); }
        }

        /// <summary>
        /// Gets or sets the pitch. Lowest possible value can be 0.1f.
        /// </summary>
        public float Pitch
        {
            get => pitch;
            set => pitch = Mathf.Max(0.1f, value);
        }

        /// <summary>
        /// Gets or sets the default clip pitch. Lowest possible value can be 0.1f.
        /// </summary>
        public float ClipPitch
        {
            get => clipPitch;
            set => clipPitch = Mathf.Max(0.1f, value);
        }

        /// <summary>
        /// A list of post processors that can be used to edit the buffer after all the audio data is compiled.
        /// </summary>
        public readonly Dictionary<string, Action<float[]>> PostProcessors = new Dictionary<string, Action<float[]>>();

        /// <summary>
        /// Gets or sets the decision on if the post processing should affect the applied Audio Clip or not.
        /// </summary>
        public bool PostProcessClip { get; set; }

        public int Channels => 2;
        public string Error => error;
        public string CurrentDevice => currentDevice;

        public static VoiceManager Instance { get; private set; }

        /// <summary>
        /// Returns a valid VoiceManager instance. If the Instance variable is null, it will create a new VoiceManager.
        /// </summary>
        /// <param name="loopLength">Length (in seconds) of the looping mic buffer.</param>
        /// <param name="device">The microphone device to be used in recording.</param>
        public static VoiceManager Get(int loopLength = 1, string device = null)
        {
            return Instance ??= new VoiceManager(loopLength, device);
        }

        /// <summary>
        /// Starts the microphone recording.
        /// </summary>
        /// <param name="device">Microphone device name to be used. If empty, the default microphone is selected.</param>
        public bool StartRecording(string device = null)
        {
            error = null;

            if (Microphone.devices == null || Microphone.devices.Length == 0)
            {
                error = "No microphone devices found";
                LogManager.LogWarning(error);
                return false;
            }

            if (string.IsNullOrEmpty(device))
                currentDevice = Microphone.devices[0];
            else
            {
                bool found = false;
                for (int i = 0; i < Microphone.devices.Length; i++)
                {
                    if (Microphone.devices[i] == device)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    error = $"Microphone device '{device}' not found";
                    LogManager.LogError(error);
                    return false;
                }

                currentDevice = device;
            }

            if (Microphone.IsRecording(currentDevice))
                Microphone.End(currentDevice);

            microphoneClip = Microphone.Start(currentDevice, true, loopLength, samplingRate);

            if (microphoneClip == null)
            {
                error = $"Failed to start microphone '{currentDevice}'";
                LogManager.LogError(error);
                return false;
            }

            lastSamplePosition = 0;
            step = (samplingRate / (float)OutputRate);
            resamplePointer = 0f;
            return true;
        }

        /// <summary>
        /// Stops the microphone recording.
        /// </summary>
        public bool StopRecording()
        {
            if (!string.IsNullOrEmpty(currentDevice) && Microphone.IsRecording(currentDevice))
                Microphone.End(currentDevice);

            microphoneClip = null;
            lastSamplePosition = 0;
            resamplePointer = 0f;
            return true;
        }

        /// <summary>
        /// Switches the microphone device and restarts recording.
        /// </summary>
        public bool SwitchMicrophone(string device)
            => StopRecording() && StartRecording(device);

        /// <summary>
        /// Restarts the microphone using the current device, or the default if none is set.
        /// </summary>
        public bool RestartMicrophone()
            => StopRecording() && StartRecording(currentDevice);

        /// <summary>
        /// Pushes an AudioClip into the output stream.
        /// </summary>
        public Guid AudioClip(AudioClip clip, bool disableMicrophone = false)
        {
            if (clip == null)
                return Guid.Empty;

            Guid id = Guid.NewGuid();

            Task.Run(() =>
            {
                try
                {
                    int channels = Mathf.Max(1, clip.channels);
                    float[] raw = new float[clip.samples * channels];
                    clip.GetData(raw, 0);

                    if (clip.frequency != OutputRate)
                        raw = Resample(raw, clip.frequency, OutputRate, channels);

                    var clipState = new Clip
                    {
                        Id = id,
                        Source = clip,
                        Samples = raw,
                        Channels = channels,
                        Position = 0f,
                        Step = 1f,
                        MuteMicrophone = disableMicrophone,
                        Gain = clipGain,
                        Pitch = clipPitch
                    };

                    lock (audioClipsLock)
                        audioClips.Add(clipState);
                }
                catch (Exception e)
                {
                    LogManager.LogError($"Failed to add audio clip: {e}");
                }
            });

            return id;
        }

        /// <summary>
        /// Resamples a raw float array to the target sample rate.
        /// </summary>
        public static float[] Resample(float[] source, int sourceRate, int targetRate, int channels)
        {
            if (source == null || source.Length == 0 || sourceRate <= 0 || sourceRate == targetRate)
                return source;

            int sourceSamples = Mathf.Max(1, source.Length / channels);
            float lengthInSeconds = (float)sourceSamples / sourceRate;
            int targetSamples = Mathf.Max(1, Mathf.RoundToInt(lengthInSeconds * targetRate));

            float[] target = new float[targetSamples * channels];

            if (sourceSamples == 1 || targetSamples == 1)
            {
                for (int c = 0; c < channels && c < target.Length; c++)
                    target[c] = source[Mathf.Clamp(c, 0, source.Length - 1)];
            }
            else
            {
                float ratio = (sourceSamples - 1f) / (targetSamples - 1f);

                for (int i = 0; i < targetSamples; i++)
                {
                    float p = i * ratio;
                    int a = Mathf.Clamp((int)p, 0, sourceSamples - 1);
                    int b = Mathf.Clamp(a + 1, 0, sourceSamples - 1);
                    float t = p - a;

                    for (int c = 0; c < channels; c++)
                    {
                        int o = i * channels + c;
                        int ia = Mathf.Clamp(a * channels + c, 0, source.Length - 1);
                        int ib = Mathf.Clamp(b * channels + c, 0, source.Length - 1);
                        target[o] = Mathf.Lerp(source[ia], source[ib], t);
                    }
                }
            }

            return target;
        }

        /// <summary>
        /// Stops the specified AudioClip from playing.
        /// </summary>
        public bool StopAudioClip(Guid id)
        {
            lock (audioClipsLock)
            {
                int index = audioClips.FindIndex(c => c.Id == id);
                if (index == -1) return false;

                audioClips.RemoveAt(index);
                return true;
            }
        }

        /// <summary>
        /// Stops all currently playing audio clips.
        /// </summary>
        public void StopAudioClips()
        {
            lock (audioClipsLock)
                audioClips.Clear();
        }

        /// <summary>
        /// Used to pull the next chunk of audio samples.
        /// Automatically called by Photon.
        /// </summary>
        public bool Read(float[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
                return false;

            if (microphoneClip == null || string.IsNullOrEmpty(currentDevice))
                return false;

            int outFrames = buffer.Length / Channels;
            int ch = microphoneClip.channels;
            int frameCount = microphoneClip.samples;
            int sampleCount = frameCount * ch;

            if (rawMicrophoneData == null || rawMicrophoneData.Length != sampleCount)
                rawMicrophoneData = new float[sampleCount];

            if (microphoneBuffer == null || microphoneBuffer.Length != buffer.Length)
                microphoneBuffer = new float[buffer.Length];

            int curFrame = Microphone.GetPosition(currentDevice);
            int lastFrame = lastSamplePosition / ch;

            int available = curFrame < lastFrame
                ? frameCount - lastFrame + curFrame
                : curFrame - lastFrame;

            float sourceStep = step * pitch;
            int needed = Mathf.CeilToInt(outFrames * sourceStep) + 2;

            if (available < needed)
                return false;

            microphoneClip.GetData(rawMicrophoneData, 0);

            bool muteMicForClip = false;
            lock (audioClipsLock)
            {
                for (int i = 0; i < audioClips.Count; i++)
                {
                    if (audioClips[i].MuteMicrophone)
                    {
                        muteMicForClip = true;
                        break;
                    }
                }
            }

            float sourcePosition = (lastSamplePosition / (float)ch) + resamplePointer;

            for (int i = 0; i < buffer.Length; i += Channels)
            {
                float left = 0f;
                float right = 0f;
                int aFrame = ((int)sourcePosition) % frameCount;
                int bFrame = (aFrame + 1) % frameCount;
                float frac = sourcePosition - Mathf.Floor(sourcePosition);

                if (!muteMicrophone && !muteMicForClip)
                {
                    if (ch == 1)
                    {
                        float a = rawMicrophoneData[aFrame];
                        float b = rawMicrophoneData[bFrame];
                        left = right = Mathf.Lerp(a, b, frac);
                    }
                    else
                    {
                        int a = aFrame * ch;
                        int b = bFrame * ch;

                        float aL = rawMicrophoneData[a];
                        float aR = rawMicrophoneData[a + 1];
                        float bL = rawMicrophoneData[b];
                        float bR = rawMicrophoneData[b + 1];

                        left = Mathf.Lerp(aL, bL, frac);
                        right = Mathf.Lerp(aR, bR, frac);
                    }
                }

                sourcePosition += sourceStep;

                buffer[i] = Mathf.Clamp(microphoneBuffer[i] + left, -1f, 1f);
                if (Channels > 1 && i + 1 < buffer.Length)
                    buffer[i + 1] = Mathf.Clamp(microphoneBuffer[i + 1] + right, -1f, 1f);
            }

            if (!PostProcessClip)
            {
                foreach (var postProcess in PostProcessors.Values)
                    postProcess?.Invoke(microphoneBuffer);
            }

            for (int i = 0; i < buffer.Length; i += Channels)
            {
                NextAudioClipSample(out float pushedLeft, out float pushedRight);

                buffer[i] = microphoneBuffer[i] + pushedLeft;
                if (Channels > 1 && i + 1 < buffer.Length)
                    buffer[i + 1] = microphoneBuffer[i + 1] + pushedRight;
            }

            if (PostProcessClip)
            {
                foreach (var postProcess in PostProcessors.Values)
                    postProcess?.Invoke(buffer);
            }

            int usedFrames = Mathf.FloorToInt(sourcePosition) - (lastSamplePosition / ch);
            lastSamplePosition = ((lastSamplePosition / ch + usedFrames) % frameCount) * ch;
            resamplePointer = sourcePosition - Mathf.Floor(sourcePosition);

            return true;
        }

        /// <summary>
        /// Returns the next left and right samples from pushed audio clips.
        /// </summary>
        private void NextAudioClipSample(out float outLeft, out float outRight)
        {
            outLeft = 0f;
            outRight = 0f;

            lock (audioClipsLock)
            {
                if (audioClips.Count == 0)
                    return;

                for (int i = audioClips.Count - 1; i >= 0; i--)
                {
                    var clip = audioClips[i];
                    int index = (int)clip.Position;
                    int maxFrames = clip.Samples.Length / clip.Channels;

                    if (index >= maxFrames)
                    {
                        audioClips.RemoveAt(i);
                        continue;
                    }

                    int nextIndex = index + 1;
                    float left = 0f;
                    float right = 0f;

                    if (nextIndex >= maxFrames)
                    {
                        if (clip.Channels == 1)
                        {
                            left = right = clip.Samples[index] * clip.Gain;
                        }
                        else
                        {
                            int baseIdx = index * clip.Channels;
                            left = clip.Samples[baseIdx] * clip.Gain;
                            right = clip.Samples[baseIdx + 1] * clip.Gain;
                        }

                        audioClips.RemoveAt(i);
                    }
                    else
                    {
                        float frac = clip.Position - index;

                        if (clip.Channels == 1)
                        {
                            left = right = Mathf.Lerp(
                                clip.Samples[index],
                                clip.Samples[nextIndex],
                                frac
                            ) * clip.Gain;
                        }
                        else
                        {
                            int baseIdx1 = index * clip.Channels;
                            int baseIdx2 = nextIndex * clip.Channels;

                            float l1 = clip.Samples[baseIdx1];
                            float r1 = clip.Samples[baseIdx1 + 1];
                            float l2 = clip.Samples[baseIdx2];
                            float r2 = clip.Samples[baseIdx2 + 1];

                            left = Mathf.Lerp(l1, l2, frac) * clip.Gain;
                            right = Mathf.Lerp(r1, r2, frac) * clip.Gain;
                        }

                        clip.Position += Mathf.Max(0.0001f, clip.Step * clip.Pitch);
                    }

                    outLeft += left;
                    outRight += right;
                }
            }
        }

        public void Dispose()
        {
            StopRecording();
            StopAudioClips();

            if (ReferenceEquals(Instance, this))
                Instance = null;
        }
    }
}