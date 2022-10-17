using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Tutorial02;

var nativeWindowSettings = new NativeWindowSettings()
{
    Size = new Vector2i(800, 600),
    Title = "Trabalho matemática e física",
    // This is needed to run on macos
    Flags = ContextFlags.ForwardCompatible,
};

using (Tutorial tutorial = new Tutorial(GameWindowSettings.Default, nativeWindowSettings))
{
    tutorial.Run();
}
