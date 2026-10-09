namespace FrmComun.FrmUtils {
    public partial class frmPreguntasSiNo : Form {
        public string Titulo {
            get => lblTitulo.Text;
            set => lblTitulo.Text = value;
        }

        public string Pregunta {
            get => lblPregunta.Text;
            set => lblPregunta.Text = value;
        }

        public string TextoAceptar {
            get => btnSi.Text;
            set => btnSi.Text = value;
        }

        public string TextoDenegar {
            get => btnNo.Text;
            set => btnNo.Text = value;
        }
        public bool NoPredeterminado { get; set; } = false;


        public frmPreguntasSiNo() {
            InitializeComponent();
            

            btnSi.Click += (s, e) => {
                DialogResult = DialogResult.Yes;
                Close();
            };

            btnNo.Click += (s, e) => {
                DialogResult = DialogResult.No;
                Close();
            };

            AcceptButton = btnNo;
            CancelButton = btnNo;

            Shown += frmPreguntasSiNo_Shown;
        }
        private void frmPreguntasSiNo_Shown(object? sender, EventArgs e) {
            btnNo.Focus();
            btnNo.Select();
        }
    }
}
