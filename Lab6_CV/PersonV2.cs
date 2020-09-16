using Lab5_CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Lab5_CV
{
    public class PersonV2: Person
    {
        private string cellPhone;
        private string instagramURL;

        public string CellPhone
        {
            get
            {
                return cellPhone;
            }
            set
            {
                //Removes all "-" from the data in contact.CellPhone
                string tempCellPhone = value.Replace("-", "");
                //Ensures the cell phone number is the right number of characters
                if (tempCellPhone.Length == 10 && tempCellPhone.All(char.IsDigit))
                {
                    cellPhone = tempCellPhone;
                }
                else
                {
                    Feedback += "\nError: The cell phone number needs to be 10 digits.";
                }
            }
        }
        public string InstagramURL
        {
            get
            {
                return instagramURL;
            }
            set
            {
                if(value.Contains("instagram.com/"))
                {
                    instagramURL = value;
                }
                else
                {
                    Feedback += "\nError: The link does not lead to instagram.com";
                }
            }
        }
        public string AddARecord()
        {
            //Init string var
            string strResult = "";

            //Make a connection object
            SqlConnection Conn = new SqlConnection();

            //Initialize it's properties
            Conn.ConnectionString = @"Server=sql.neit.edu,4500;Database=SE245_CViens;User Id=SE245_CViens;Password=008008773;";     //Set the Who/What/Where of DB

            //*******************************************************************************************************
            // NEW
            //*******************************************************************************************************
            string strSQL = "INSERT INTO Persons (FirstName, MiddleName, LastName, Street1, Street2, City, State, Zip, Phone, Email, CellPhone, InstagramURL) VALUES (@FirstName, @MiddleName, @LastName, @Street1, @Street2, @City, @State, @Zip, @Phone, @Email, @CellPhone, @InstagramURL)";
            // Bark out our command
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSQL;  //Commander knows what to say
            comm.Connection = Conn;     //Where's the phone?  Here it is

            //Fill in the paramters (Has to be created in same sequence as they are used in SQL Statement)
            comm.Parameters.AddWithValue("@FirstName", FirstName);
            comm.Parameters.AddWithValue("@MiddleName", MiddleName);
            comm.Parameters.AddWithValue("@LastName", LastName);
            comm.Parameters.AddWithValue("@Street1", Street1);
            comm.Parameters.AddWithValue("@Street2", Street2);
            comm.Parameters.AddWithValue("@City", City);
            comm.Parameters.AddWithValue("@State", State);
            comm.Parameters.AddWithValue("@Zip", Zip);
            comm.Parameters.AddWithValue("@Phone", Phone);
            comm.Parameters.AddWithValue("@Email", Email);
            comm.Parameters.AddWithValue("@CellPhone", CellPhone);
            comm.Parameters.AddWithValue("@InstagramURL", InstagramURL);

            //*******************************************************************************************************


            //attempt to connect to the server
            try
            {
                Conn.Open();                                        //Open connection to DB - Think of dialing a friend on phone
                int intRecs = comm.ExecuteNonQuery();
                strResult = $"SUCCESS: Inserted {intRecs} to Database";       //Report that we made the connection
                Conn.Close();                                       //Hanging up after phone call
            }
            catch (Exception err)                                   //If we got here, there was a problem connecting to DB
            {
                strResult = "ERROR: " + err.Message;                //Set feedback to state there was an error & error info
            }
            finally
            {

            }

            //Return resulting feedback string
            return strResult;
        }

        //Constructor for the PersonV2 class using the Person Constructor as a base
        public PersonV2(): base()
        {
            cellPhone = "";
            instagramURL = "";
        }
    }
}
