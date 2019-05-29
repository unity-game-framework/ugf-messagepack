using System;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace MessagePack.Patcher
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // ../MessagePack/bin/Debug/netstandard2.0/MessagePack.dll

                Patch(args[1]);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Console.ReadKey();
            }
            finally
            {
                Console.Write("Done");
            }
        }

        private static void Patch(string path)
        {
            using (var stream = new MemoryStream(File.ReadAllBytes(path)))
            {
                AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(stream);
                TypeDefinition type = assembly.MainModule.Types.First(x => x.Name == "MessagePackUnsafeUtility");
                MethodDefinition method = type.Methods.First(x => x.Name == "As");
                Collection<Instruction> instructions = method.Body.Instructions;

                instructions.Clear();
                instructions.Add(Instruction.Create(OpCodes.Ldarg_0));
                instructions.Add(Instruction.Create(OpCodes.Ret));

                assembly.Write(path);
            }
        }
    }
}
