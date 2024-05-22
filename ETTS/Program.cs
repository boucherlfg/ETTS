// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;

int speed = 140;
string language = "fr";

GetConfig();

if (args.Length <= 0)
{
    NoArgs();
}
else
{
    FromFile();
}

void GetConfig()
{
    if (File.Exists("config.tts"))
    {
        foreach (var line in File.ReadAllLines("config.tts"))
        {
            string param = line.Split('=').First().Trim();
            string value = line.Split('=').Last().Trim();

            switch (param)
            {
                case nameof(speed):
                    speed = int.Parse(value);
                    break;
                case nameof(language):
                    language = value;
                    break;
                default:
                    break;
            }
        }
    }
}

void FromFile()
{
    var content = File.ReadAllText(string.Join(" ", args), Encoding.UTF8);
    Say(content);
}
void NoArgs()
{
    while (true)
    {
        Console.Write(" > ");
        var input = Console.ReadLine();
        Say(input);
    }
}

void Say(string message) 
{
    new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "espeak",
            Arguments = $"-v{language} -s{speed} \"{message}\""
        }
    }.Start();
}
