using System.Diagnostics;

var startInfo = new ProcessStartInfo();
startInfo.FileName = "cmd.exe";
startInfo.RedirectStandardInput = true;
startInfo.RedirectStandardOutput = true;
startInfo.UseShellExecute = false;

var process = Process.Start(startInfo);

Console.WriteLine("Enter your project name:");
var projectName = Console.ReadLine();

Console.WriteLine("Do you want to add git ? YES/NO");
var addGit = Console.ReadLine()!.ToLower().Contains("yes") ? true : false;

Console.WriteLine("Do you want to add gitignore ? YES/NO");
var addGitignore = Console.ReadLine()!.ToLower().Contains("yes") ? true : false;


Console.WriteLine("Creating...");
try { process!.StandardInput.WriteLine($"dotnet new sln --name {projectName}"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet new webapi --name {projectName}.Api"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet new classlib --name {projectName}.Application"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet new classlib --name {projectName}.Domain"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet new classlib --name {projectName}.Infrastructure"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet new classlib --name {projectName}.Persistence"); } catch (Exception ex) { Console.WriteLine(ex.Message); }

try { process!.StandardInput.WriteLine($"dotnet sln {projectName}.sln add {projectName}.Api/{projectName}.Api.csproj"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet sln {projectName}.sln add {projectName}.Application/{projectName}.Application.csproj"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet sln {projectName}.sln add {projectName}.Domain/{projectName}.Domain.csproj"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet sln {projectName}.sln add {projectName}.Infrastructure/{projectName}.Infrastructure.csproj"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet sln {projectName}.sln add {projectName}.Persistence/{projectName}.Persistence.csproj"); } catch (Exception ex) { Console.WriteLine(ex.Message); }


try { process!.StandardInput.WriteLine($"dotnet add {projectName}.Api/{projectName}.Api.csproj reference {projectName}.Infrastructure/{projectName}.Infrastructure.csproj"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet add {projectName}.Infrastructure/{projectName}.Infrastructure.csproj reference {projectName}.Persistence/{projectName}.Persistence.csproj"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet add {projectName}.Infrastructure/{projectName}.Infrastructure.csproj reference {projectName}.Application/{projectName}.Application.csproj"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet add {projectName}.Persistence/{projectName}.Persistence.csproj reference {projectName}.Domain/{projectName}.Domain.csproj"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
try { process!.StandardInput.WriteLine($"dotnet add {projectName}.Application/{projectName}.Application.csproj reference {projectName}.Domain/{projectName}.Domain.csproj"); } catch (Exception ex) { Console.WriteLine(ex.Message); }

if (addGit)
    try { process!.StandardInput.WriteLine("git init"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
if (addGitignore)
    try { process!.StandardInput.WriteLine("dotnet new gitignore"); } catch (Exception ex) { Console.WriteLine(ex.Message); }


process!.WaitForExit();
process!.Close();

Console.WriteLine("Done :)");

Console.WriteLine("Press any key to exit");
Console.ReadKey();