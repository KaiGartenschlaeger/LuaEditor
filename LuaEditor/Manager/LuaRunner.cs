using LuaEditor.Objetcts;
using System;
using System.Diagnostics;
using System.Text;

namespace LuaEditor.Manager
{
    public class LuaRunner
    {
        #region Methods

        public string Run(ProjectSettings project)
        {
            StringBuilder output = new StringBuilder();
            Stopwatch sw = new Stopwatch();

            Process process = new Process();

            int exitCode = -1;
            bool timedOut = false;
            TimeSpan executionTime = TimeSpan.Zero;

            try
            {
                process.StartInfo.FileName = ApplicationPath;
                process.StartInfo.WorkingDirectory = project.RootDirectory;

                string startArguments = ApplicationArguments;
                startArguments = startArguments.Replace("{projectRootPath}", "\"" + project.RootDirectory + "\"");
                if (project.StartEntry != null)
                {
                    startArguments = startArguments.Replace("{startElementPath}", "\"" + project.StartEntry.Location + "\"");
                }
                else
                {
                    startArguments = startArguments.Replace("{startElementPath}", string.Empty);
                }

                process.StartInfo.Arguments = startArguments;

                if (HideWindow)
                {
                    process.StartInfo.CreateNoWindow = true;
                }

                process.StartInfo.UseShellExecute = false;

                process.EnableRaisingEvents = true;

                if (RedirectOutput)
                {
                    process.StartInfo.RedirectStandardOutput = true;
                    process.OutputDataReceived += (sender, e) =>
                    {
                        output.AppendLine(e.Data);
                    };

                    process.StartInfo.RedirectStandardError = true;
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        output.AppendLine(e.Data);
                    };
                }

                sw.Start();
                process.Start();

                if (RedirectOutput)
                {
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                }

                timedOut = !process.WaitForExit(5000);

                sw.Stop();

                exitCode = process.ExitCode;
                executionTime = sw.Elapsed;
            }
            catch (Exception ex)
            {
                output.AppendLine("Fehler bei Ausführung: \"" + ex.Message + "\"");
            }
            finally
            {
                process.Close();
            }

            output.AppendLine($"Ausführung abgeschlossen in {executionTime.TotalMilliseconds} ms. (Exit Code = {exitCode})");

            return output.ToString();
        }

        #endregion

        #region Properties

        public string ApplicationPath { get; set; }
        public string ApplicationArguments { get; set; }
        public bool RedirectOutput { get; set; }
        public bool HideWindow { get; set; }

        #endregion
    }
}