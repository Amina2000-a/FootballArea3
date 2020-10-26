using FootballArea.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballArea
{
    public partial class ReservArea_Dashboard : Form
    {
        FootballAreaDBEntities db = new FootballAreaDBEntities();
        decimal totalPrice;
        public ReservArea_Dashboard()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            string FirstName = txtFirstname.Text;
            string LastName = txtLastname.Text;
            string Phone = txtPhone.Text;
            string AreaName = cmbAreaname.Text;
            string AreaNumber = cmbAreanumber.Text;
            string RoomNumber = cmbRoomnumber.Text;
            DateTime DateFrom = dateFrom.Value;
            DateTime DateTo = dateTo.Value;


            int PhoneNumber;

            if (Extensions.isNotEmpty(new string[]{
                FirstName,LastName,Phone,AreaName,AreaNumber,RoomNumber
            }, string.Empty))
            {

                if (db.Rooms.Any(m => m.PersonCount <= 12))
                {
                    if (int.TryParse(Phone, out PhoneNumber))
                    {
                        Rooms selectedRoom = db.Rooms.First(a => a.Room_Number.ToString() == RoomNumber);
                        Areas selectedArea = db.Areas.First(b => b.Area_Name == AreaName);
                        totalPrice = selectedArea.Price;

                        Customer selectCustomer = null;
                        int CustomerID = 0;

                        Task ClientTask = Task.Factory.StartNew(() =>
                        {

                            selectCustomer = db.Customer.Add(new Customer
                            {
                                Firstname = FirstName,
                                Lastname = LastName,
                                Phone = Convert.ToString(PhoneNumber)
                            });
                            db.SaveChanges();

                        });


                        ClientTask.Wait();
                        if (ClientTask.IsCompleted)
                        {
                            CustomerID = selectCustomer.ID;
                        }

                        Reservation rv = db.Reservation.Add(new Reservation
                        {
                            Reservation_From = DateFrom,
                            Reservation_To = DateTo,
                            Customer_Id = CustomerID,
                            Area_Id = selectedArea.ID,
                            Rooms_Id = selectedRoom.ID,
                            Price = (int)totalPrice

                        });
                        db.SaveChanges();






                        MessageBox.Show("Reservation was added", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Please enter correct phone number";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "This room if full";




                }
            }


            else
            {
                lblError.Visible = true;
                lblError.Text = "Please field all input";
            }

        }

        private void ReservArea_Dashboard_Load(object sender, EventArgs e)
        {
            FillcmbAreaname();
            FillcmbAreanumber();
            FillcmbRoomnumber();
        }

       
        private void FillcmbAreaname()
        {
            cmbAreaname.Items.AddRange(db.Areas.Select(pn => pn.Area_Name).ToArray());
        }
        private void FillcmbAreanumber()
        {
            cmbAreanumber.Items.AddRange(db.Areas.Select(a => a.Area_Number).ToArray());
        }
        private void FillcmbRoomnumber()
        {
            cmbRoomnumber.Items.AddRange(db.Rooms.Select(b => b.Rooms_Number).ToArray());
        }
        private void CmbRoomnumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPrice.Text = totalPrice.ToString();
        }
    }
    }



