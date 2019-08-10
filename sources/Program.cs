﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFTriadBuddy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            Logger.Initialize(Args);

            bool bUpdatePending = GithubUpdater.FindAndApplyUpdates();
            if (bUpdatePending)
            {
                return;
            }

            bool bInit = Form1.InitializeGameAssets();
            if (bInit)
            {
                if (Args.Contains("-dataConvert"))
                {
                    DataConverter converter = new DataConverter();
                    converter.Run();
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
            }
            else
            {
                MessageBox.Show("Failed to initialize! Make sure that " + AssetManager.Get().DBRelativePath + " is next to executable.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}