using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoNWebApp
{
    public partial class TestSql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Server=184.168.194.75; Database=goyals420_hawk; User Id=cishawk; password=cis420hawk");


            SqlDataSource sqldb = new SqlDataSource();

            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Students(FirstName,LastName,MiddleName, EmailAddress, PhoneNumber, Address, City, State, ZipCode, Standing, CampusID, EnrollmentDate, StudentNumber, GPA) VALUES('Z', 'D', 'O', 'email', 'phone', 'address', 'city', 'state', 40511, 0, 11, 1 / 2 / 2015, 10101, 3); ", conn);

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}