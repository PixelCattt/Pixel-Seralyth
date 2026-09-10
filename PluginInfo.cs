/*
** Pixelyth-Menu - PluginInfo.cs
** An Open-Source Mod Menu for Gorilla Tag with 2000+ Mods!
**
** Copyright (C) 2026 - PixelCatt
** https://github.com/PixelCattt/Pixelyth-Menu
**
** This program is free software: you can redistribute it and/or modify
** it under the terms of the GNU General Public License as published by
** the Free Software Foundation, either version 3 of the License, or
** (at your option) any later version.
**
** This program is distributed in the hope that it will be useful,
** but WITHOUT ANY WARRANTY; without even the implied warranty of
** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
** See the GNU General Public License for more details.
**
** You should have received a copy of the GNU General Public License
** along with this program.
** If not, see <https://www.gnu.org/licenses/>.
*/

// New Pixelyth Changes: https://github.com/Pixelyth/Pixelyth-Menu/compare/57bf9df...master
// All PSM Changes: https://github.com/PixelCattt/Pixels-Pixelyth-Menu/compare/57bf9df...master

namespace Pixelyth
{
    public class PluginInfo
    {
        public const string GUID = "com.pixelcatt.pixelyth";
        public const string Name = "Pixelyth-Menu";
        public const string Description = "An Open-Source Mod Menu for Gorilla Tag with 2000+ Mods!";
        public const string BuildTimestamp = "2026-09-09@20-23-33";
        public const string Version = "9.0.0";

        public const string BaseDirectory =
#if LEGAL || LEGAL_DEBUG
            "PixelythMenu/Legal";
#else
            "PixelythMenu";
#endif

        public const string ClientResourcePath = "Pixelyth-Menu.Resources.Client";
        public const string ServerResourcePath = "https://raw.githubusercontent.com/PixelCattt/Pixelyth-Menu/master/Resources/Server";
        public const string ServerAPI = "https://menu.Pixelyth.software";
        public const string Logo = @"
                                            %%%%%                                                   
                                           %%% %%%%                                                 
                                         %%%      %%%%                                              
                                        %%%         %%%%        %%%  %                              
                                      %%%%            %%%%%%%% %%%%  %%                             
                                     %%%        %#####% %%%%%        %%                             
                                    %%%       ############ %%%                                      
                                  %%%       ######     %###  %%%%     %%%                           
                                %%%%       ######        ###   %#%%    %%                           
                             %%%#%        ######         ###%    %#%%                               
                       %%%%  %%#%         ######         %###      %##% %%                          
                 %%%%  %%   %##           ######%         ##%         %###%                         
                           %#%             ######        ###            ###%                        
                         %##%              %######%    #####              ###%                      
#%   %##                  #######%                        ###                    
                   %% %##                     %#######%                        ###%                 
###                        %########%                       ###%               
###                            %#######%                       %##%             
                  %##                                %#######%                        ###           
                %##%                                   %#######%                     ###%           
###                   %##########%        #######%                   ###             
##%                  %####%    %####        %######%                ###               
###                  %###%        %##%         %######%              ###                
###                 ###%          %%%           %######%            ##%                 
###              %###                          #######          ####                  
                %###           ####                          #######        %###                    
####         ####                          #######       ###   ##                 
                    %###       ####                         %######       ##%    ##%                
###      ###                         ######      ###                         
                         %###   ####                       ######      ###        %%%               
####  %####                   %######     ###           #%               
                            %%###% ####%              ########      ##%         %%%                 
###%%######%%    %#########%      ###     %%%% %%%%                 
                             %#   %### %###############%         ##%%%%% %%%%                       
                              %%    %##%                       %##  %                               
                                       %##                    %#%                                   
                               %%        %#%%               %%%%                                    
                               %%%         %%#%            %%%                                      
                                      %%%%%  %%%%        %%%%                                       
                                 %%%%           %%%     %%%                                         
                                                  %%%% %%%                                          
                                                    %%%%                                            ";

#if DEBUG || LEGAL_DEBUG
        public static bool BetaBuild = true;
#else
        public static bool BetaBuild = false;
#endif
    }
}
