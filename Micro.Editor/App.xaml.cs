﻿using System;
using System.Windows;

namespace Micro.Editor
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if (DEBUG)
            RunInDebugMode();
#else
            RunInReleaseMode();
#endif
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private static void RunInDebugMode()
        {
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += (o, e) => HandleException(e.ExceptionObject as Exception);
            try
            {
                Bootstrapper bootstrapper = new Bootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null)
                return;

            //ExceptionPolicy.HandleException(ex, "Default Policy");
            //MessageBox.Show(StockTraderRI.Properties.Resources.UnhandledException);
            //Environment.Exit(1);
        }
    }
}
