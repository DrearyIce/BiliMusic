using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BiliMusic.Models
{
    public static class util_Console
    {
        public static async Task<string> Regex (string inputFileName, util_Console_Action action)
        {
            StreamReader reader;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = EnvironmentVariables.AppPath + "\\Igex.exe";
            startInfo.Arguments = $" -{action.ToString()} {inputFileName}";
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            Process process = Process.Start(startInfo);
            reader = process.StandardOutput;
            string output = "";
            while (!reader.EndOfStream)
               output = reader.ReadToEnd();
            await process.WaitForExitAsync();
            return output;
        }

        public static async Task<FileInfo> DownloadSong(Song song)
        {
            DirectoryInfo directory = new DirectoryInfo(EnvironmentVariables.SavesPath + "\\" + song.AId);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = EnvironmentVariables.BBDownPath + "\\BBDown.exe";
            startInfo.Arguments = $"{util_BvConverter.Encode(long.Parse(song.AId))} " +
                $"--work-dir {EnvironmentVariables.SavesPath}\\{song.AId} " +
                $"--audio-only --file-pattern Song";
            startInfo.CreateNoWindow= true;
            Process proc = Process.Start(startInfo);
            await proc.WaitForExitAsync();
            foreach (FileInfo file in directory.GetFiles())
                if (file.Extension == ".m4a")
                {
                    File.Move(file.FullName, file.DirectoryName + "\\Song.m4a");
                    util_File.SaveFile_SongJson(song);
                    return file;
                }
            int SongIndex = 0;
            
            return null;
        }

        public static async Task Download(string args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = EnvironmentVariables.BBDownPath + "\\BBDown.exe";
            startInfo.Arguments = args;
            startInfo.CreateNoWindow = true;
            Process proc = Process.Start(startInfo);
            await proc.WaitForExitAsync();
        }

        public static async Task BBDownLogin(bool isTVlogin)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = EnvironmentVariables.BBDownPath + "\\BBDown.exe";
            startInfo.Arguments = "login";
            if (isTVlogin) startInfo.Arguments = "logintv";
            startInfo.CreateNoWindow = false;
            Process proc = Process.Start(startInfo);
            await proc.WaitForExitAsync();
        }
    }

    public enum util_Console_Action
    {
        RankSong,
        Recommends
    }
}
