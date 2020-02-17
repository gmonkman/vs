using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using MyFunctions.FileSystem;
using EntityFramework.BulkInsert.Extensions;

namespace FolderAnalysis
{
	public partial class frmMain : Form
	{

		#region Declarations
		long m_fileCount = 0;
		long m_currentFileCount = 0;
		long m_folderCount = 0;
		long m_currentFolderCount = 0;
		Guid m_sessionid;
		FileType _FileType = new MyFunctions.FileSystem.FileType();
		const long Q_LEN = 50;

		public struct AddMetric
		{
			public double totalTimeSeconds;
			public Queue<double> last10Times;
		}

		private AddMetric m_addMetric;
		#endregion


		#region Form Events etc
		public frmMain()
		{
			InitializeComponent();
			lblStatus.Text = "";
			lblMetric.Text = "";
			textBox1.Text = "U:\\College of Natural Sciences\\Research Data\\OCEAN-PROJECT-EFF";
			m_addMetric.last10Times = new Queue<double>();
			btnCancel.Enabled = false;
			progressBar1.Value = 0;
			progressBar1.Minimum = 0;
			progressBar1.Maximum = 100;
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult dr = fbd.ShowDialog();
				switch (dr)
				{
					case DialogResult.OK:
						textBox1.Text = fbd.SelectedPath;
						break;
					default:
						textBox1.Text = "";
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			try
			{
				if (textBox1.Text == "")
				{
					MessageBox.Show("You must select a folder.", "Select Folder", MessageBoxButtons.OK);
					return;
				}

				using (var db = new edmFolderAnalysisContainer())
				{
					db.Configuration.AutoDetectChangesEnabled = false;
					db.Configuration.ValidateOnSaveEnabled = false;
					if (chkDelete.Checked)
					{
						db.Database.ExecuteSqlCommand("Delete from attributes;");
						db.Database.ExecuteSqlCommand("delete from files;");
						db.Database.ExecuteSqlCommand("Delete from sessions;");
					}
					var sess = new session();
					sess.sessionid = System.Guid.NewGuid();
					sess.run_time = DateTime.Today;
					sess.root = textBox1.Text;
					sess.completed = false;
					db.sessions.Add(sess);
					db.SaveChanges();
					m_sessionid = sess.sessionid;
				}
				SetCounts();
				IterateFolder(textBox1.Text);
				_FileType.WriteNewExtensionsToFile();
				MessageBox.Show("Completed", "Completed", MessageBoxButtons.OK);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.lblStatus.Text = "";
				lblMetric.Text = "";
				btnCancel.Enabled = false;
				m_sessionid = Guid.Empty;
				m_currentFileCount = m_currentFolderCount = m_fileCount = m_folderCount = progressBar1.Value = 0;
			}
		}
		#endregion


		#region Methods and Functions

		private void IterateFolder(string folder)
		{
			DirectoryInfo[] dirs;
			DirectoryInfo di = new DirectoryInfo(folder);
			dirs = di.GetDirectories("*", SearchOption.AllDirectories);
			foreach (DirectoryInfo d in dirs)
			{
				try
				{
					WriteFiles(d.FullName);
				}
				catch
				{ }
				++m_currentFolderCount;
			}
		}

		private void WriteFiles(string folder)
		{
			FileInfo[] files;
			DirectoryInfo di = new DirectoryInfo(folder);

			files = di.GetFiles("*", SearchOption.TopDirectoryOnly);
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			using (edmFolderAnalysisContainer db = new edmFolderAnalysisContainer())
			{
				db.Configuration.AutoDetectChangesEnabled = false;
				db.Configuration.ValidateOnSaveEnabled = false;
				foreach (FileInfo fi in files)
				{
					progressBar1.Value = (int)(100 * (decimal)m_currentFileCount / m_fileCount);
					Application.DoEvents();
					string fullName = folder + "\\" + fi.Name;
					if (!this.FileExistsInDB(folder, fi.Name))
					{
						try
						{

							#region Add To Files Table
							sw.Start();
							db.Database.BeginTransaction();
							var dbFiles = new files();
							dbFiles.filesid = Guid.NewGuid();
							dbFiles.file_name_full = fullName;
							dbFiles.file = fi.Name;
							dbFiles.folder = folder;
							dbFiles.session_sessionid = m_sessionid;
							db.files.Add(dbFiles);
							db.SaveChanges();

							var dbAttr = new FolderAnalysis.attribute();
							dbAttr.files_filesid = dbFiles.filesid;
							dbAttr.attributeid = Guid.NewGuid();
							dbAttr.name = "Size";
							dbAttr.value = fi.Length.ToString();
							db.attributes.Add(dbAttr);

							dbAttr = new FolderAnalysis.attribute();
							dbAttr.files_filesid = dbFiles.filesid;
							dbAttr.attributeid = Guid.NewGuid();
							dbAttr.name = "LastWriteTime";
							dbAttr.value = fi.LastWriteTime.ToString("yyyymmdd hh:mm");
							db.attributes.Add(dbAttr);

							dbAttr = new FolderAnalysis.attribute();
							dbAttr.files_filesid = dbFiles.filesid;
							dbAttr.attributeid = Guid.NewGuid();
							dbAttr.name = "LastAccessTime";
							dbAttr.value = fi.LastAccessTime.ToString("yyyymmdd hh:mm");
							db.attributes.Add(dbAttr);

							dbAttr = new FolderAnalysis.attribute();
							dbAttr.files_filesid = dbFiles.filesid;
							dbAttr.attributeid = Guid.NewGuid();
							dbAttr.name = "Extension";
							dbAttr.value = fi.Extension;
							db.attributes.Add(dbAttr);

							dbAttr = new FolderAnalysis.attribute();
							dbAttr.files_filesid = dbFiles.filesid;
							dbAttr.attributeid = Guid.NewGuid();
							dbAttr.name = "Type";
							dbAttr.value = _FileType.GetFileType(fi.Name);
							db.attributes.Add(dbAttr);

							#endregion

							#region Add Attributes for File
							ShellObject so = ShellObject.FromParsingName(fullName);
							if (so != null)
							{
								foreach (var sp in so.Properties.DefaultPropertyCollection)
								{
									try //try catch in an attempt to improve performance
									{
										string val = sp.ValueAsObject.ToString();
										string name = (sp.CanonicalName == null) ? "" : sp.CanonicalName.ToString();
										if (val != "" && name != "")
										{
											dbAttr = new FolderAnalysis.attribute();
											dbAttr.files_filesid = dbFiles.filesid;
											dbAttr.attributeid = Guid.NewGuid();
											dbAttr.name = name;
											dbAttr.value = val;
											db.attributes.Add(dbAttr);
										}
									}
									catch { }
								}
							}
							db.SaveChanges();
							db.Database.CurrentTransaction.Commit();
							#endregion

						}
						catch (Exception)
						{
							if (db.Database.CurrentTransaction != null) db.Database.CurrentTransaction.Rollback();
						}
						finally
						{
							if (db.Database.CurrentTransaction != null) db.Database.CurrentTransaction.Dispose();
							sw.Stop();
							if (m_addMetric.last10Times.Count >= Q_LEN) m_addMetric.last10Times.Dequeue();
							m_addMetric.last10Times.Enqueue((double)sw.ElapsedMilliseconds / 1000);
							m_addMetric.totalTimeSeconds += (double)sw.ElapsedMilliseconds / 1000;
							sw.Reset();
						}
						double a = (double)m_addMetric.totalTimeSeconds / m_currentFileCount;
						double b = (double)m_addMetric.last10Times.Average() * (m_fileCount - m_currentFileCount); //use last Q_LEN time trend to estimate remaining time
						string s = TimeSpan.FromSeconds(b).ToString().Substring(0, 8);
						lblMetric.Text = string.Format(
							"Global file add time(s): {0:N2}. Trending add time(s): {1:N2}.  Estimated time to complete (d.hh:mm): {2}",
							a,
							m_addMetric.last10Times.Average(),
							s
							);
					}
					++m_currentFileCount;
					lblStatus.Text = string.Format("Processed {0} files of {1} from {2} folders of {3}.", m_currentFileCount, m_fileCount, m_currentFolderCount, m_folderCount);
					Application.DoEvents();
				}

			}
		}

		private void SetCounts()
		{
			DirectoryInfo di = new DirectoryInfo(this.textBox1.Text);
			m_fileCount = di.GetFiles("*", SearchOption.AllDirectories).LongLength;
			m_folderCount = di.GetDirectories("*", SearchOption.AllDirectories).LongLength;
		}

		private bool FileExistsInDB(string folder, string file)
		{
			using (edmFolderAnalysisContainer db = new edmFolderAnalysisContainer())
			{
				int obj = db.Database.SqlQuery<int>(@"SELECT 1 as one FROM [files] WHERE [file]={0} AND [folder]={1}", file, folder).FirstOrDefault<int>();
				return (obj == 1);
			}
		}
		#endregion
	}
}
