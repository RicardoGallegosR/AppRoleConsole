namespace Apps_Administrativa {
    internal static class Program {
        [STAThread]
        static void Main() {
            ApplicationConfiguration.Initialize();
            Application.Run(new Formularios.Home());
        }
    }
}