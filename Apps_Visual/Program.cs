namespace Apps_Visual {
    internal static class Program {
        [STAThread]
        static void Main() {
            ApplicationConfiguration.Initialize();
            Application.Run(new ObdAppGUI.frmBASE());
            //Application.Run(new ObdAppGUI.Views.frmAuth());
            //Application.Run(new ObdAppGUI.Views.frmCapturaVisual());
            //Application.Run(new ObdAppGUI.Views.frmOBD());
        }
    }
}