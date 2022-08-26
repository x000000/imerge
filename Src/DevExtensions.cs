using Avalonia;
using Avalonia.Controls;

namespace x0.imerge;

public static class DevExtensions
{
#if DEBUG
    public static void TryAttachDevTools(this Window wnd)
    {
        if (Application.Current?.ApplicationLifetime is DesktopApplicationLifetime desktop
            && desktop.Options.AttachDevTools == typeof(MainWindow).FullName
        ) {
            wnd.AttachDevTools();
        }
    }
#endif
}