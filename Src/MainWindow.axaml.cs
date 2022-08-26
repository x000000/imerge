using Avalonia.Controls;

namespace x0.imerge
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.TryAttachDevTools();
#endif
        }
    }
}