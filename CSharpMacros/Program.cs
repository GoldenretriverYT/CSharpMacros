using System.Text.RegularExpressions;

namespace CSharpMacros {
    internal class Program {
        static void Main(string[] args) {
            if (args.Length != 1) {
                Console.WriteLine("Usage: CSharpMacros.exe <path to file>");
                return;
            }

            var path = args[0];

            if (!File.Exists(path)) {
                Console.WriteLine($"File {path} does not exist");
                return;
            }

            var text = File.ReadAllText(path);
            string[] lines = text.Split(Environment.NewLine);
            var macros = new Dictionary<string, (List<string> parameters, string body)>();

            for(int i = 0; i < lines.Length; i++) {
                var line = lines[i];
                if (line.StartsWith("/*macro ")) {
                    string macroDefinitionLiteral = line;

                    List<string> parameters = line.Split(" ").Skip(1).ToList();
                    string name = parameters[0];

                    parameters.RemoveAt(0);
                    string body = "";
                    while (i < lines.Length-1) {
                        i++;
                        line = lines[i];
                        macroDefinitionLiteral += Environment.NewLine + line;

                        if (line.EndsWith("*/"))
                            break;
                        
                        body += line + Environment.NewLine;
                    }

                    macros.Add(name, (parameters, body));

                    text = text.Replace(macroDefinitionLiteral, "");
                }
            }

            // remove everything between #region MacroDummies and #endregion
            text = Regex.Replace(text, @"#region MacroDummies.*#endregion", "", RegexOptions.Singleline);

            foreach (var macro in macros) {
                var name = macro.Key;
                var parameters = macro.Value.parameters;
                var body = macro.Value.body;

                var regex = new Regex($"\\b{name}\\b\\s*\\((.*?)\\)");
                var matches = regex.Matches(text);

                foreach (Match match in matches) {
                    var matchText = match.Value;
                    var matchParameters = match.Groups[1].Value.Split(",").Select(x => x.Trim()).ToList();

                    var newBody = body;
                    for (int i = 0; i < parameters.Count; i++) {
                        newBody = newBody.Replace(parameters[i], matchParameters[i]);
                    }

                    text = text.Replace(matchText, newBody);
                }
            }

            File.WriteAllText("macro_" + path, text);
        }
    }
}