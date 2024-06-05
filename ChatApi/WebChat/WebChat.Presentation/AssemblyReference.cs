using System.Reflection;

namespace WebChat.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}