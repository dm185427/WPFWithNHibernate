using NHibernate;
using PlayerInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PlayersInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetPlayerInfo();
        }
        private void GetPlayerInfo()
        {
            try
            {
                using (ISession session = SessionFactory.OpenSession)
                {
                    var play=session.Query<Player>().ToList();
                    PlayersGrid.ItemsSource = play;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DisplayPlayer(Player ply)
        {
            try
            {
                txtPId.Text = ply.Id.ToString();
                txtPname.Text = ply.PlayerName;
                txtPAge.Text = ply.PlayerAge.ToString();
                txtDoj.Text = ply.DOJ.ToString();
                txtBelongsTo.Text = ply.BelongsTo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Player AddPlayer()
        {
            Player p = new Player();
            p.PlayerName = txtPname.Text;
            p.PlayerAge = Convert.ToInt32(txtPAge.Text);
            p.DOJ = Convert.ToDateTime(txtDoj.Text);
            p.BelongsTo = txtBelongsTo.Text;
            return p;
        }
        private void btnUpadate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ISession session = SessionFactory.OpenSession)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        Player player = PlayersGrid.SelectedItem as Player;
                        Player p = AddPlayer();
                        p.Id = player.Id;
                        session.Update(p);
                        transaction.Commit();
                        MessageBox.Show("Player Details have been Updated");
                        GetPlayerInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                var player = PlayersGrid.SelectedItem;
                DisplayPlayer(player as Player);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ISession session = SessionFactory.OpenSession)
                {
                    Player p = AddPlayer();
                    session.Save(p);
                    MessageBox.Show("Player Details have been added");
                    ClearFields();
                    GetPlayerInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ISession session = SessionFactory.OpenSession)
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        var player = PlayersGrid.SelectedItem as Player;
                        session.Delete(player);
                        transaction.Commit();
                        MessageBox.Show("Player Details have been deleted");
                        GetPlayerInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            txtPname.Text = string.Empty;
            txtPAge.Text = string.Empty;
            txtDoj.Text = DateTime.Now.ToString();
            txtBelongsTo.Text = "India";
        }
    }
}
