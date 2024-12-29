using System;
using Sys = Cosmos.System;
using CorgiOS.Core;
using System.Threading;
using System.IO;
using System.Text;

namespace CorgiOS.Commands
{
    class File : Command
    {
        public File(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            if (args.Length < 1)
                return "Usage: file <Command>";

            switch (args[0])
            {
                case "mk":
                    try
                    {
                        if (args.Length != 2)
                            return "Usage: file mk <path>";
                        Sys.FileSystem.VFS.VFSManager.CreateFile(Kernel.Path + args[1]);
                        return "Created file \"" + args[1] + "\" successfully.";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                case "rm":
                    try
                    {
                        if (args.Length != 2)
                            return "Usage: file rm <path>";
                        Sys.FileSystem.VFS.VFSManager.DeleteFile(Kernel.Path + args[1]);
                        return "Removed file \"" + args[1] + "\" successfully.";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                case "mkdir":
                    try
                    {
                        if (args.Length != 2)
                            return "Usage: file mkdir <path>";
                        Sys.FileSystem.VFS.VFSManager.CreateDirectory(Kernel.Path + args[1]);
                        return "Created directory \"" + args[1] + "\" successfully.";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                case "rmdir":
                    try
                    {
                        if (args.Length != 2)
                            return "Usage: file rmdir <path>";
                        Sys.FileSystem.VFS.VFSManager.DeleteDirectory(Kernel.Path + args[1], true);
                        return "Removed directory \"" + args[1] + "\" successfully.";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                case "space":
                    try
                    {
                        return "Free Space: " + (Kernel.vfs.GetAvailableFreeSpace(Kernel.Path) / 1024).ToString() + " KB";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                case "format":
                    try
                    {
                        try
                        {
                            Sys.FileSystem.VFS.VFSManager.DeleteDirectory("0:\\", true);
                        }
                        catch { }
                        if (Kernel.vfs.Disks[0].Partitions.Count > 0)
                            Kernel.vfs.Disks[0].DeletePartition(0);
                        Kernel.vfs.Disks[0].Clear();
                        Kernel.vfs.Disks[0].CreatePartition((int)Kernel.vfs.Disks[0].Size / (1024 * 1024));
                        Kernel.vfs.Disks[0].FormatPartition(0, "FAT32", true);
                        ACPIManager.Reboot();
                        return "";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                case "getfsformat":
                    try
                    {
                        var fs_type = Kernel.vfs.GetFileSystemType(@"0:\");
                        return fs_type;
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                case "write":
                    try
                    {
                        if (args.Length < 3)
                            return "Usage: file write <Path> <Text>";
                        if (!Sys.FileSystem.VFS.VFSManager.FileExists(Kernel.Path + args[1]))
                            return "File does not exist.";
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(Kernel.Path + args[1]).GetFileStream();
                        if (fs.CanWrite)
                        {
                            int ctr = 0;
                            StringBuilder sb = new StringBuilder();

                            foreach (string arg in args)
                            {
                                if (ctr > 1)
                                    sb.Append(arg + ' ');
                                ctr++;
                            }
                            byte[] data = Encoding.ASCII.GetBytes(sb.ToString().Replace("\\n", "\n"));
                            fs.Write(data, 0, data.Length - 1);
                            fs.Close();
                            return "Wrote to file \"" + args[1] + "\" successfully.";
                        }
                        return "Can not write to read-only/closed file.";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                case "read":
                    try
                    {
                        if (args.Length != 2)
                            return "Usage: file read <Path>";
                        if (!Sys.FileSystem.VFS.VFSManager.FileExists(Kernel.Path + args[1]))
                            return "File does not exist.";
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(Kernel.Path + args[1]).GetFileStream();
                        if (fs.CanRead)
                        {
                            byte[] data = new byte[fs.Length];
                            fs.Read(data, 0, data.Length);
                            fs.Close();
                            return Encoding.ASCII.GetString(data);
                        }
                        return "Can not read from write-only/closed file.";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                case "pwd":
                    return Kernel.Path;
                case "cd":
                    if (args.Length > 1)
                    {
                        if (args[1] == "..")
                        {
                            if (Kernel.Path != "0:\\")
                            {
                                string tempPath = Kernel.Path.Substring(0, Kernel.Path.Length - 1);
                                Kernel.Path = tempPath.Substring(0, tempPath.LastIndexOf("\\"));
                                if (Kernel.Path == "0:")
                                    Kernel.Path = "0:\\";
                            }
                            return "";
                        }
                        string path = args[1];
                        if (!path.Contains("\\"))
                            path = Kernel.Path + path + "\\";
                        if (!path.EndsWith("\\"))
                            path += "\\";
                        if (Sys.FileSystem.VFS.VFSManager.DirectoryExists(path))
                            Kernel.Path = path;
                        else
                            return "Directory " + path + " not found!";
                    }
                    else
                        Kernel.Path = "0:\\";
                    return "";
                case "ls":
                    var files_list = Directory.GetFiles(Kernel.Path);
                    var directory_list = Directory.GetDirectories(Kernel.Path);
                    StringBuilder resultBuilder = new StringBuilder();

                    foreach (var file in files_list)
                    {
                        resultBuilder.Append(file + "\n");
                    }
                    foreach (var directory in directory_list)
                    {
                        resultBuilder.AppendLine(directory + "\n");
                    }
                    return resultBuilder.ToString();
                case "copy":
                    try
                    {
                        if (args.Length != 3)
                            return "Usage: file copy <Path> <Path>";
                        if (!Sys.FileSystem.VFS.VFSManager.FileExists(Kernel.Path + args[1]))
                            return "File does not exist.";
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(Kernel.Path + args[1]).GetFileStream();
                        if (fs.CanRead)
                        {
                            byte[] data = new byte[fs.Length];
                            fs.Read(data, 0, data.Length);
                            fs.Close();
                            Sys.FileSystem.VFS.VFSManager.CreateFile(Kernel.Path + args[2]);
                            FileStream fs2 = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(Kernel.Path + args[2]).GetFileStream();
                            if (fs2.CanWrite)
                            {
                                fs2.Write(data, 0, data.Length - 1);
                                fs2.Close();
                                return "Copyed file \"" + args[1] + "\" successfully.";
                            }
                        }
                        return "Can not copy write-only/read-only/closed file.";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                case "move":
                    try
                    {
                        if (args.Length != 3)
                            return "Usage: file move <Path> <Path>";
                        if (!Sys.FileSystem.VFS.VFSManager.FileExists(Kernel.Path + args[1]))
                            return "File does not exist.";
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(Kernel.Path + args[1]).GetFileStream();
                        if (fs.CanRead)
                        {
                            byte[] data = new byte[fs.Length];
                            fs.Read(data, 0, data.Length);
                            fs.Close();
                            Sys.FileSystem.VFS.VFSManager.CreateFile(Kernel.Path + args[2]);
                            FileStream fs2 = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(Kernel.Path + args[2]).GetFileStream();
                            if (fs2.CanWrite)
                            {
                                fs2.Write(data, 0, data.Length - 1);
                                fs2.Close();
                                Sys.FileSystem.VFS.VFSManager.DeleteFile(Kernel.Path + args[1]);
                                return "Moved file \"" + args[1] + "\" successfully.";
                            }
                        }
                        return "Can not copy write-only/read-only/closed file.";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                default:
                    return "Invaild arg.";
            }
        }
    }
}
