using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knjizara_Milance_NRT820
{
    internal class DataBase
    {
        public static string CONNECTION_STRING = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\milan\source\repos\Knjizara.accdb";
        OleDbConnection connection;

        public OleDbConnection Connection { get => connection; set => connection = value; }

        public DataBase()
        {
            connection = new OleDbConnection(CONNECTION_STRING);
        }
        public void OpenConnection()
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                }
            }
            catch
            {
                MessageBox.Show(null,"Neuspela konekcija sa bazom podataka!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            catch
            {
                MessageBox.Show(null, "Neuspela diskonekcija od baze podataka!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
