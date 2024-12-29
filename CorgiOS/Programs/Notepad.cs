using System;
using CorgiOS.Core;
using CorgiOS.Graphics.JXWS;
using System.Drawing;
using Sys = Cosmos.System;
using System.IO;
using System.Text;

namespace CorgiOS.Programs
{
    public class Notepad : Window
    {
        public Text textBox, tempFileEnterPath, temp2;
        private bool EnteredFile = false;
        public string FilePath = "";

        public Notepad(int x, int y, int width, int height) : base(x, y, width, height, "Notepad")
        {
            this.textBox = new Text(0, 0, "", Color.White);
            this.tempFileEnterPath = new Text(0, 0, "", Color.White);
            this.temp2 = new Text(0, height - (Window.WindowTopSize + Cosmos.System.Graphics.Fonts.PCScreenFont.Default.Height + 1), "", Color.White);
            this.addComponent(this.textBox);
            this.addComponent(this.tempFileEnterPath);
            this.addComponent(this.temp2);
        }

        public override void onKeyPress(Cosmos.System.KeyEvent keyData)
        {
            if (!EnteredFile)
            {
                this.temp2.text = " ^ Enter File Path";
                if (keyData.Key == Cosmos.System.ConsoleKeyEx.Escape)
                {
                    this.Close();
                }
                else if (keyData.Key == Cosmos.System.ConsoleKeyEx.Enter || keyData.Key == Cosmos.System.ConsoleKeyEx.NumEnter)
                {
                    this.FilePath = Kernel.Path + tempFileEnterPath.text;
                    try
                    {
                        if (!Sys.FileSystem.VFS.VFSManager.FileExists(FilePath))
                        {
                            Sys.FileSystem.VFS.VFSManager.CreateFile(FilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.temp2.text = ex.Message;
                        return;
                    }
                    this.temp2.text = "Press ESC To Save.";
                    this.tempFileEnterPath.text = "";
                    try
                    {
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(FilePath).GetFileStream();
                        if (fs.CanRead && fs.CanWrite)
                        {
                            byte[] data = new byte[fs.Length];
                            fs.Read(data, 0, data.Length);
                            fs.Close();
                            this.textBox.text = Encoding.ASCII.GetString(data);
                        }
                        else
                            throw new Exception("File Is Closed, Read Only Or Write Only.");
                    }
                    catch (Exception ex)
                    {
                        this.temp2.text = ex.Message;
                        return;
                    }
                    this.EnteredFile = true;
                }
                else if (keyData.Key == Cosmos.System.ConsoleKeyEx.Backspace)
                {
                    this.tempFileEnterPath.text = this.tempFileEnterPath.text.Remove(this.tempFileEnterPath.text.Length - 1, 1);
                }
                else
                {
                    this.tempFileEnterPath.text += keyData.KeyChar;
                }
            }
            else
            {
                if (keyData.Key == Cosmos.System.ConsoleKeyEx.Escape)
                {
                    try
                    {
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(FilePath).GetFileStream();
                        if (fs.CanWrite)
                        {
                            byte[] data = Encoding.ASCII.GetBytes(this.textBox.text);
                            fs.Write(data, 0, data.Length - 1);
                            fs.Close();
                        }
                        else
                        {
                            this.temp2.text = "We Cant Save Your File Due To It Being Closed.";
                            return;
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
                else if (keyData.Key == Cosmos.System.ConsoleKeyEx.Enter)
                {
                    this.textBox.text += "\n";
                }
                else if (keyData.Key == Cosmos.System.ConsoleKeyEx.Backspace)
                {
                    this.textBox.text = this.textBox.text.Remove(this.textBox.text.Length - 1, 1);
                }
                else
                {
                    this.textBox.text += keyData.KeyChar;
                }
            }
        }
    }
}
