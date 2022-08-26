using System;
using Avalonia.Markup.Xaml;

namespace x0.imerge;

public class ViewModelExtension : MarkupExtension
{
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        var pvt = (IProvideValueTarget) serviceProvider.GetService(typeof(IProvideValueTarget))!;

        try {
            var fullName = pvt.TargetObject.GetType().FullName;
            var vmType = Type.GetType(fullName + "ViewModel");
            return Activator.CreateInstance(vmType ?? throw new InvalidOperationException("No view model found for " + fullName));
        } catch {
            return null;
        }
    }
}