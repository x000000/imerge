using System.IO;
using Avalonia;
using Avalonia.Media.Imaging;

namespace x0.imerge;

public class MainWindowViewModel
{
    public MinimalImageRevs Images { get; }

    private readonly NamedStartupOptions? _opts;

    public MainWindowViewModel()
        : this(Application.Current?.ApplicationLifetime is DesktopApplicationLifetime desktop ? desktop.Options : null)
    {
    }

    public MainWindowViewModel(NamedStartupOptions? opts)
    {
        if (opts != null) {
            _opts = opts;

            var baseRev = TryReadImage("BASE", _opts.Base);
            var local   = ReadImage("LOCAL",   _opts.Local);
            var remote  = ReadImage("REMOTE",  _opts.Remote);

            Images = baseRev == null
                ? new MinimalImageRevs(local, remote)
                : new ImageRevs(baseRev, local, remote);
        }
    }

    public void TakeRev(object param)
    {
        try {
            File.Copy(((ImageRev) param).Source, _opts!.Merged, true);
            App.Shutdown();
        } catch {
            App.Shutdown(-1);
        }
    }

    private ImageRev? TryReadImage(string revType, string? path)
        => path != null && File.Exists(path)
            ? ReadImage(revType, path)
            : null;

    private ImageRev ReadImage(string revType, string path)
    {
        try {
            return new ImageRev(path, new Bitmap(path));
        } catch {
            throw new IOException($"Unable to read {revType} file: {path}");
        }
    }
}

public record ImageRev(string Source, Bitmap Bitmap);
public record MinimalImageRevs(ImageRev Local, ImageRev Remote);
public record ImageRevs(ImageRev Base, ImageRev Local, ImageRev Remote) : MinimalImageRevs(Local, Remote);
