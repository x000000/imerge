using Avalonia.Controls.ApplicationLifetimes;

namespace x0.imerge;

public class DesktopApplicationLifetime : ClassicDesktopStyleApplicationLifetime
{
    public NamedStartupOptions Options { get; }

    public DesktopApplicationLifetime(NamedStartupOptions options) => Options = options;
}