using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace PendriveScan
{
    public partial class Interface : Form
    {
        //=================================================
        // Atributos
        //=================================================
        private List<DriveInfo> Infodrivers = new List<DriveInfo>();
        //=================================================

        //=================================================
        // Metodo construtor
        //=================================================
        public Interface()
        {
            InitializeComponent();
            listAllDrives();
        }
        //=================================================

        //=================================================
        // Obter lista de dispositivos removiveis
        //=================================================
        private void listAllDrives()
        {
            List<String> _drivesDataSource = new List<string>();
            Infodrivers.Clear();
            foreach(DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    _drivesDataSource.Add(drive.Name);
                    Infodrivers.Add(drive);
                }             
            }
            if (Infodrivers.Capacity > 0)
            {
                this.List_Drivers.DataSource = _drivesDataSource.ToArray();
                this.btiniciar.Enabled = true;
            }
        }
        //=================================================

        //=================================================
        // Converte o pendrive caso necessario
        //=================================================
        private bool checkPendriveFormat(int _indexDrive)
        {
            if (Infodrivers[_indexDrive].DriveFormat != "NTFS")
            {
                if(MessageBox.Show("Seu pendrive está em um formato de alto risco, podendo ser facilmente infectado por virús." +
                                   "\nDeseja converter seu pendrive ? Nenhum dado será perdido.", "Pendrive Scan", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        changeListaTxt("\nConvertendo pendrive...");
                        ProcessStartInfo formatPendrive = new ProcessStartInfo("convert", Infodrivers[_indexDrive].Name.ToCharArray()[_indexDrive] + ": /fs:ntfs");
                        formatPendrive.CreateNoWindow = false;
                        formatPendrive.WindowStyle = ProcessWindowStyle.Normal;
                        Process processFormat = Process.Start(formatPendrive);
                        processFormat.WaitForExit();
                        changeListaTxt("OK\n");
                        return true;
                    }catch(Exception e)
                    {
                        MessageBox.Show("Mensagem de erro: " + e.Message, "Erro no processo de conversão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }                                     
                }
            }
            return true;
        }
        //=================================================

        //=================================================
        // Criar pasta temporaria
        //=================================================
        private void createprotectfolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            setProtect(new DirectoryInfo(path), "Todos", FileSystemRights.Delete, AccessControlType.Deny);
        }
        //=================================================

        //=================================================
        // Mover todo conteudo da raiz para a pasta de proteção
        //=================================================
        private void movetofolder(DirectoryInfo _directory, string _tothisfolder)
        {       
            foreach(FileInfo file in _directory.GetFiles())
            {
                try
                {
                    file.MoveTo(_tothisfolder + @"\" + file.Name);
                }
                catch { }
            }     
            foreach (DirectoryInfo directory in _directory.GetDirectories())
            {               
                try
                {
                    changeListaTxt(directory.FullName + " - OK\n");
                    Directory.Move(directory.FullName, _tothisfolder + @"\" + directory.Name);
                    
                }
                catch { }
            }
        }
        //=================================================

        //=================================================
        // Setar configurações de acesso a pasta raiz
        //=================================================
        private void setProtect(DirectoryInfo _directory, string _account, FileSystemRights _rights , AccessControlType _controltype)
        {
            changeListaTxt("\nConfigurando proteções...");
            DirectorySecurity _security = _directory.GetAccessControl();
             _security.AddAccessRule(new FileSystemAccessRule(_account, _rights, _controltype));
            _directory.SetAccessControl(_security);            
            changeListaTxt("Concluido..!\n");
        }
        //=================================================

        //=================================================
        // Modificar textos
        //=================================================
        private void changeListaTxt(string _text)
        {
            this.Lista.AppendText(_text);
            this.Lista.ScrollToCaret();
        }
        //=================================================

        //=================================================
        // Obtem numero de arquivos para o progess bar
        //=================================================
        private void contfiles(DirectoryInfo _directory)
        {
            foreach (DirectoryInfo sub in _directory.GetDirectories())
                {
                try
                {
                    this.ScanProgress.Maximum += sub.GetFiles("*", SearchOption.AllDirectories).Length + sub.GetDirectories("*", SearchOption.AllDirectories).Length;
                }
                catch { }
                }
            this.ScanProgress.Maximum += _directory.GetDirectories().Length + _directory.GetFiles().Length;
        }
        //=================================================

        //================================================='                                                                    
        // Botão iniciar
        //=================================================
        private void button1_Click(object sender, EventArgs e)
        {
            this.ScanProgress.Value = 0;
            this.Lista.Text = "";
            scandirectory(this.List_Drivers.Text);
        }
        //=================================================

        //=================================================
        // Botão atualizar
        //=================================================
        private void btatualizar_Click(object sender, EventArgs e)
        {
            listAllDrives();
        }
        //=================================================

        //=================================================
        // Deletar arquivos suspeitos
        //=================================================
        private void checkFileDanger(FileInfo _file)
        {
            if((_file.Name.StartsWith("{") && _file.Name.EndsWith("}") && _file.Extension.StartsWith(".{") && _file.Extension.EndsWith("}")) || _file.Extension.Length > 6)
            {
                if(MessageBox.Show("Arquivo suspeito detectado: " + _file.Name + "\nEste arquivo pode ser potencialmente perigoso para o seu" +
                                   "pendrive e computador, recomenda-se que você o exclua caso não o reconheça. Deseja excluir este arquivo ? ", "Aviso", 
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        changeListaTxt(_file.FullName + " - Deletado\n");
                        _file.Delete();
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show("Falha ao deletar arquivo. \n Erro: " + e.Message + ".\nPossivelmente este arquivo é do seu sistema operacional e não necessita ser excluido", "Scan Pendrive", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //=================================================

        //=================================================
        // Checar arquivos
        //=================================================
        private void checkFiles(FileInfo file)
        {
            if (file.Extension.Equals(".lnk", StringComparison.CurrentCultureIgnoreCase))
            {
                changeListaTxt(file.FullName + " - Removido\n");
                file.Delete();
            }
            else
            {
                try {
                    file.Attributes = file.Attributes.HasFlag(FileAttributes.System) ? FileAttributes.Hidden : FileAttributes.Normal;
                }
                catch { }
                changeListaTxt(file.FullName + " - OK\n");
                checkFileDanger(file);
            }
            this.ScanProgress.Value++;
        }
        //=================================================

        //=================================================
        // Checar diretorios
        //=================================================
        private void checkDirectories(DirectoryInfo  _direct)
        {           
            _direct.Attributes = _direct.Name.Equals("System Volume Information") ? FileAttributes.Hidden : FileAttributes.Normal;

            foreach (DirectoryInfo _subdirectory in _direct.GetDirectories("*", SearchOption.AllDirectories))
            {

                if (_subdirectory.Name.Equals("System Volume Information")) _subdirectory.Attributes = FileAttributes.Hidden;
                else
                {
                    _subdirectory.Attributes = FileAttributes.Normal;
                    changeListaTxt(_subdirectory.FullName + " - OK\n");
                }
                this.ScanProgress.Value++;
            }
        }
        //=================================================

        //=================================================
        // Prossesso de escaneamento do pendrive
        //=================================================
        private void scandirectory(string path)
        {
            //=============================================
            // Criar atributos de acesso ao dispositivo
            //=============================================
            DirectoryInfo _directory = new DirectoryInfo(path);
            DirectorySecurity _security = _directory.GetAccessControl();
            string pathsecurityfolder = path + @"\" + this.nm_folder.Text + @"\";
            //=============================================

            contfiles(_directory);

            //=============================================
            // Escanear arquivos da raiz
            //=============================================
            foreach (FileInfo file in _directory.GetFiles())
            {
                checkFiles(file);
            }
            //=============================================

            //=============================================
            // Desocultar todos os arquivos do dispositivo subpastas
            //=============================================
            foreach (DirectoryInfo _direct in _directory.GetDirectories())
            {
                checkDirectories(_direct);
                try
                {                    
                    foreach (FileInfo file in _direct.GetFiles("*", SearchOption.AllDirectories))
                    {
                        checkFiles(file);
                    }
                }catch(Exception ex) { MessageBox.Show(ex.Message); }
                this.ScanProgress.Value++;
            }
            //=============================================

            //=============================================
            // Checar se existe a pasta sem nome
            //=============================================
            if (Directory.Exists(_directory.Name + @"\" + Convert.ToChar(160) + @"\"))
            {
                movetofolder(new DirectoryInfo(_directory.Name + @"\" + Convert.ToChar(160) + @"\"), _directory.FullName);
                Directory.Delete(_directory.Name + @"\" + Convert.ToChar(160) + @"\");
            }
            //=============================================

            //=============================================
            // Proteger ou não o pendrive
            //=============================================
            if (MessageBox.Show("Deseja proteger seu pendrive ?\nATENÇÂO: Apos esse processo não será mais possivel guardar arquivos na pasta inicial do" +
                                "seu pendrive, uma pasta será criada e todos os seus arquivos serão movidos para ela, desse modo seu pendrive estará protegido.",
                                "Scan Pendrive", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (checkPendriveFormat(this.List_Drivers.SelectedIndex))
                {
                    createprotectfolder(pathsecurityfolder);
                    movetofolder(_directory, pathsecurityfolder);
                    setProtect(_directory, "Todos", FileSystemRights.CreateDirectories | FileSystemRights.CreateFiles | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.WriteAttributes, AccessControlType.Deny);
                }
                else
                {
                    MessageBox.Show("Não foi possivel converter seu pendrive para um formato seguro, seu dispositivo ainda corre riscos de infecção do virus", "Scan Pendrive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //=============================================

            //=============================================
            // Finalizar processo
            //=============================================
            this.ScanProgress.Value = this.ScanProgress.Maximum;
            changeListaTxt("\nProcesso concluido.\n");
            Process.Start(_directory.FullName);
            //=============================================

        }
        //=================================================

        //=================================================
        // Botão de informações
        //=================================================
        private void bt_sobre_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este aplicativo visa escanear e proteger seu pendrive.\nCriado por: Brendon Oliveira");
        }
        //=================================================
    }
}
