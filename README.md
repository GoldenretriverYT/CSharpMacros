# CSharpMacros
 Bringing Macros to CSharp, primarily for Cosmos to prevent method calling performance overhead

# WORK IN PROGRESS!
This is still work in progress.

# How to build my project using CSharpMacros?
1. Download the latest release or build it yourself.
2. Add the folder to your PATH environment variable.
3. Right click your project, go to Build -> Build Events
4. Set following values:
  - Prebuild: `CSharpMacros.exe $(ProjectDir) apply`
  - Postbuild: `CSharpMacros.exe $(ProjectDir) restore`
  - When to run build events: `Always`
5. Build your project and enjoy the performance boost!

# Macro Definition
To define a new macro, use following syntax:
```csharp
/*macro MyMacro x y
	(x+y)
*/
```

Example:
```csharp
/*macro PlusAndStringify x y
	(x+y).ToString()
*/
```

# Macro Usage
To use a macro, use following syntax (its just like a method call):
```csharp
MyMacro(1, 2);

// Example usage:
Console.WriteLine(PlusAndStringify(1,2));
// -> will extend to:
Console.WriteLine((1+2).ToString());
```

# My IDE is complaining!
It is recommended to add dummy methods to prevent this. CSharpMacros automatically removes everything in the "MacroDummies" region.

Example:
```csharp
#region MacroDummies
static void MyMacro(int x, int y){} // Dummy method to prevent IDE complaining
#endregion
```