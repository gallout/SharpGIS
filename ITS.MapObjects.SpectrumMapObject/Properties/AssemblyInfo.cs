using System.Reflection;
using System.Runtime.InteropServices;
using ITS.MapObjects.BaseMapObject.Misc.PluginAttributes;
using ITS.MapObjects.SpectrumMapObject;

[assembly: AssemblyTitle("ITS.MapObjects.SpectrumMapObject")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("ITS.MapObjects.SpectrumMapObject")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Параметр ComVisible со значением FALSE делает типы в сборке невидимыми 
// для COM-компонентов.  Если требуется обратиться к типу в этой сборке через 
// COM, задайте атрибуту ComVisible значение TRUE для этого типа.

[assembly: ComVisible(false)]

// Следующий GUID служит для идентификации библиотеки типов, если этот проект будет видимым для COM

[assembly: Guid("c9f29841-c5ed-4cc8-9dcf-c859e1090d6a")]
[assembly: UnityConfigResolver(typeof (VeloObjectConfigResolver))]