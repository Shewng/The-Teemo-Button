using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.IO;

namespace TeemoHoverTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Fields

        /// <summary>
        /// Hold the current result of a cell in the game
        /// </summary>
        private CellType[] gridSpots;

        /// <summary>
        /// Keeps track of the player's current health
        /// </summary>
        private int health;

        /// <summary>
        /// Random number to place shrooms
        /// </summary>
        private readonly Random rand = new Random();

        /// <summary>
        /// Mediaplayers for multiple sound overlays
        /// </summary>
        #region Mediaplayers
        private readonly MediaPlayer mediaPlayer1 = new MediaPlayer();
        private readonly MediaPlayer mediaPlayer2 = new MediaPlayer();
        private readonly MediaPlayer mediaPlayer3 = new MediaPlayer();
        private readonly MediaPlayer mediaPlayer4 = new MediaPlayer();
        private readonly MediaPlayer mediaPlayer5 = new MediaPlayer();
        private readonly MediaPlayer mediaPlayer6 = new MediaPlayer();
        private readonly MediaPlayer mediaPlayer7 = new MediaPlayer();
        #endregion

        private string dir = ((Directory.GetCurrentDirectory()).Replace(@"\bin\Debug", @"\")).Replace(@"\", @"\\");

        #endregion

        #region Assets

        /// <summary>
        /// The picture of Teemo's shroom
        /// </summary>
        private ImageBrush CreateShroom()
        {
            // Grabbing the picture of the shroom
            ImageBrush img = new ImageBrush();
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(new Uri(dir + "\\Images\\mushroom-transparent.png").AbsoluteUri);
            bmp.EndInit();
            img.ImageSource = bmp;
            img.Opacity = 1;

            return img;
        }
        
        /// <summary>
        /// The picture of the Teemo button
        /// </summary>
        private ImageBrush CreateTeemoButton()
        {
            // Grabbing the picture of the shroom
            ImageBrush img = new ImageBrush();
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(new Uri(dir + "\\Images\\teemobutton.png").AbsoluteUri);
            bmp.EndInit();
            img.ImageSource = bmp;
            img.Opacity = 1;

            return img;
        }

        /// <summary>
        /// The picture of the Devil Teemo button
        /// </summary>
        private ImageBrush CreateDevilTeemoButton()
        {
            // Grabbing the picture of the shroom
            ImageBrush img = new ImageBrush();
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(new Uri(dir + "\\Images\\devilteemobutton.png").AbsoluteUri);
            bmp.EndInit();
            img.ImageSource = bmp;
            img.Opacity = 1;

            return img;
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Use specified MediaPlayer to play an audio file
        /// Path must be absolute, user should change string in mp.Open(new System.Uri("<path>" + filename));
        /// </summary>
        /// <param name="mp">Which mediaplayer to use</param>
        /// <param name="filename">The file that contains the audio to play</param>
        private void PlayAudio(MediaPlayer mp, String filename)
        {
            mp.Open(new System.Uri(dir + "\\TeemoSounds\\" + filename));
            mp.Play();
        }

        /// <summary>
        /// Keyboard shortcut to close the window
        /// </summary>
        /// <param name="sender">The key that was pressed</param>
        /// <param name="e">The events of the key press</param>
        private void Key_CloseWindow(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            // Helper method
            this.KeyUp += Key_CloseWindow;
            InitializeComponent();

            // Create a new game upon application launch
            NewGame();
        }

        #endregion

        /// <summary>
        /// Starts a new game while resetting all values back to the original state
        /// </summary>
        private void NewGame()
        {
            // Create a blank array of free cells
            gridSpots = new CellType[104];

            for (int i = 0; i < gridSpots.Length; i++)
                gridSpots[i] = CellType.Free;

            // Iterate every rectangle on the grid to clear
            GridContainer.Children.OfType<Rectangle>().ToList().ForEach(rect =>
            {
                // Restore/reset background
                rect.Fill = Brushes.White;
            });

            // Reset Teemo
            Teemo.Fill = CreateTeemoButton();
            Teemo.Margin = new Thickness(645, 250, 0, 0);
            Teemo.Visibility = Visibility.Visible;
            Teemo.Opacity = 1;

            // Reset Devil Teemo
            //Devil_Teemo.Visibility = Visibility.Collapsed;
            Devil_Teemo.Width = 5;
            Devil_Teemo.Height = 5;
            Devil_Teemo.Opacity = 0;

            // Make the health bar visible
            Health_Bar_Front.Visibility = Visibility.Visible;
            Health_Bar_Back.Visibility = Visibility.Visible;
            Health_Text.Visibility = Visibility.Visible;

            // Make the mana bar visible
            Mana_Bar_Front.Visibility = Visibility.Visible;
            Mana_Bar_Back.Visibility = Visibility.Visible;
            Mana_Text.Visibility = Visibility.Visible;

            // Set the player's health bar
            health = 2375;
            Health_Bar_Front.Width = 550;

            // Place the initial invisible shrooms on the board
            PlaceInitShrooms();

        }

        /// <summary>
        /// Place the initial shrooms on the field
        /// </summary>
        private void PlaceInitShrooms()
        {
            #region Shroom randomization
            
            // Grab the image of the shroom
            ImageBrush shroom = CreateShroom();

            // Generate a random number for every section of the grid (3 spots in a row, for now)
            int r0 = rand.Next(0, 3);
            int r1 = rand.Next(3, 6);
            int r2 = rand.Next(6, 9);
            int r3 = rand.Next(9, 12);
            int r4 = rand.Next(12, 15);
            int r5 = rand.Next(15, 18);
            int r6 = rand.Next(18, 21);
            int r7 = rand.Next(21, 24);
            int r8 = rand.Next(24, 27);
            int r9 = rand.Next(27, 30);
            int r10 = rand.Next(30, 33);
            int r11 = rand.Next(33, 36);
            int r12 = rand.Next(36, 39);
            int r13 = rand.Next(39, 42);
            int r14 = rand.Next(42, 45);
            int r15 = rand.Next(45, 48);
            int r16 = rand.Next(48, 51);
            int r17 = rand.Next(51, 54);
            int r18 = rand.Next(54, 57);
            int r19 = rand.Next(57, 60);
            int r20 = rand.Next(60, 63);
            int r21 = rand.Next(63, 66);
            int r22 = rand.Next(66, 69);
            int r23 = rand.Next(69, 72);
            int r24 = rand.Next(72, 75);
            int r25 = rand.Next(75, 78);
            int r26 = rand.Next(78, 81);
            int r27 = rand.Next(81, 84);
            int r28 = rand.Next(84, 87);
            int r29 = rand.Next(87, 90);
            int r30 = rand.Next(90, 92);
            int r31 = rand.Next(93, 96);
            int r32 = rand.Next(96, 99);
            int r33 = rand.Next(99, 102);
            int r34 = rand.Next(102, 104);


            // Randomly select one of the three spaces in each specific section (3 in a row)
            var cells0 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r0);
            var cells1 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r1);
            var cells2 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r2);
            var cells3 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r3);
            var cells4 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r4);
            var cells5 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r5);
            var cells6 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r6);
            var cells7 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r7);
            var cells8 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r8);
            var cells9 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r9);
            var cells10 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r10);
            var cells11 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r11);
            var cells12 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r12);
            var cells13 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r13);
            var cells14 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r14);
            var cells15 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r15);
            var cells16 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r16);
            var cells17 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r17);
            var cells18 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r18);
            var cells19 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r19);
            var cells20 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r20);
            var cells21 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r21);
            var cells22 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r22);
            var cells23 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r23);
            var cells24 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r24);
            var cells25 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r25);
            var cells26 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r26);
            var cells27 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r27);
            var cells28 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r28);
            var cells29 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r29);
            var cells30 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r30);
            var cells31 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r31);
            var cells32 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r32);
            var cells33 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r33);
            var cells34 = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r34);

            // Place them officially by swapping their images to shrooms
            // Make them invisible
            cells0.Fill = shroom; cells0.Opacity = 0.01;
            cells1.Fill = shroom; cells1.Opacity = 0.01;
            cells2.Fill = shroom; cells2.Opacity = 0.01;
            cells3.Fill = shroom; cells3.Opacity = 0.01;
            cells4.Fill = shroom; cells4.Opacity = 0.01;
            cells5.Fill = shroom; cells5.Opacity = 0.01;
            cells6.Fill = shroom; cells6.Opacity = 0.01;
            cells7.Fill = shroom; cells7.Opacity = 0.01;
            cells8.Fill = shroom; cells8.Opacity = 0.01;
            cells9.Fill = shroom; cells9.Opacity = 0.01;
            cells10.Fill = shroom; cells10.Opacity = 0.01;
            cells11.Fill = shroom; cells11.Opacity = 0.01;
            cells12.Fill = shroom; cells12.Opacity = 0.01;
            cells13.Fill = shroom; cells13.Opacity = 0.01;
            cells14.Fill = shroom; cells14.Opacity = 0.01;
            cells15.Fill = shroom; cells15.Opacity = 0.01;
            cells16.Fill = shroom; cells16.Opacity = 0.01;
            cells17.Fill = shroom; cells17.Opacity = 0.01;
            cells18.Fill = shroom; cells18.Opacity = 0.01;
            cells19.Fill = shroom; cells19.Opacity = 0.01;
            cells20.Fill = shroom; cells20.Opacity = 0.01;
            cells21.Fill = shroom; cells21.Opacity = 0.01;
            cells22.Fill = shroom; cells22.Opacity = 0.01;
            cells23.Fill = shroom; cells23.Opacity = 0.01;
            cells24.Fill = shroom; cells24.Opacity = 0.01;
            cells25.Fill = shroom; cells25.Opacity = 0.01;
            cells26.Fill = shroom; cells26.Opacity = 0.01;
            cells27.Fill = shroom; cells27.Opacity = 0.01;
            cells28.Fill = shroom; cells28.Opacity = 0.01;
            cells29.Fill = shroom; cells29.Opacity = 0.01;
            cells30.Fill = shroom; cells30.Opacity = 0.01;
            cells31.Fill = shroom; cells31.Opacity = 0.01;
            cells32.Fill = shroom; cells32.Opacity = 0.01;
            cells33.Fill = shroom; cells33.Opacity = 0.01;
            cells34.Fill = shroom; cells34.Opacity = 0.01;


            // This is the important part
            // Randomly selected spots become shroomed
            gridSpots[r0] = CellType.Shroomed;
            gridSpots[r1] = CellType.Shroomed;
            gridSpots[r2] = CellType.Shroomed;
            gridSpots[r3] = CellType.Shroomed;
            gridSpots[r4] = CellType.Shroomed;
            gridSpots[r5] = CellType.Shroomed;
            gridSpots[r6] = CellType.Shroomed;
            gridSpots[r7] = CellType.Shroomed;
            gridSpots[r8] = CellType.Shroomed;
            gridSpots[r9] = CellType.Shroomed;
            gridSpots[r10] = CellType.Shroomed;
            gridSpots[r11] = CellType.Shroomed;
            gridSpots[r12] = CellType.Shroomed;
            gridSpots[r13] = CellType.Shroomed;
            gridSpots[r14] = CellType.Shroomed;
            gridSpots[r15] = CellType.Shroomed;
            gridSpots[r16] = CellType.Shroomed;
            gridSpots[r17] = CellType.Shroomed;
            gridSpots[r18] = CellType.Shroomed;
            gridSpots[r19] = CellType.Shroomed;
            gridSpots[r20] = CellType.Shroomed;
            gridSpots[r21] = CellType.Shroomed;
            gridSpots[r22] = CellType.Shroomed;
            gridSpots[r23] = CellType.Shroomed;
            gridSpots[r24] = CellType.Shroomed;
            gridSpots[r25] = CellType.Shroomed;
            gridSpots[r26] = CellType.Shroomed;
            gridSpots[r27] = CellType.Shroomed;
            gridSpots[r28] = CellType.Shroomed;
            gridSpots[r29] = CellType.Shroomed;
            gridSpots[r30] = CellType.Shroomed;
            gridSpots[r31] = CellType.Shroomed;
            gridSpots[r32] = CellType.Shroomed;
            gridSpots[r33] = CellType.Shroomed;
            gridSpots[r34] = CellType.Shroomed; 

            #endregion
        }


        /// <summary>
        /// Move Teemo depending on the cursor and margin of the screen
        /// </summary>
        /// <param name="sender">Touching Teemo with the mouse</param>
        /// <param name="e">The events of the touch</param>
        private async void Move_Teemo(object sender, MouseEventArgs e)
        {
            // Get the x and y coordinates of the mouse pointer
            System.Windows.Point position = e.GetPosition(this);
            double pX = position.X;
            double pY = position.Y;

            // Move in small increments so it's not choppy
            for (int i = 0; i < 5; i++)
            {

                #region Movement logic

                Thickness margin = Teemo.Margin;
                // Push Teemo based on mouse position
                if (pX >= margin.Left - 50 && pX <= margin.Left + 50)   // push teemo from left
                {
                    if (margin.Left >= 1300) // If at right border of window, shift left
                    {
                        margin.Left -= 200;
                    }
                    else
                    {
                        margin.Left += 10;
                    }
                }
                if (pX <= margin.Left + 200 && pX >= margin.Left + 150) // push teemo from right
                {
                    if (margin.Left <= 0)  // If at left border of window, shift right
                    {
                        margin.Left += 200;
                    }
                    else
                    {
                        margin.Left -= 10;
                    }
                }
                if (pY >= margin.Top - 50 && pY <= margin.Top + 50)      // push teemo from top
                {
                    if (margin.Top >= 600)  // If at bottom border of window, shift up
                    {
                        margin.Top -= 200;
                    }
                    else
                    {
                        margin.Top += 10;
                    }
                }
                if (pY <= margin.Top + 200 && pY >= margin.Top + 150)   // push teemo from bottom
                {
                    if (margin.Top <= 0)    // If at top border of window, shift down
                    {
                        margin.Top += 200;
                    }
                    else
                    {
                        margin.Top -= 10;
                    }
                }

                Teemo.Margin = margin;
                await Task.Delay(1);

                #endregion

                #region Laugh logic

                // 50% chance of Teemo laughing while running
                int laughChance = 5;
                int randnum = rand.Next(10);
                int laughType = rand.Next(3);

                // Teemo may laugh if you touch him; different kinds of laughs
                if (randnum == laughChance && laughType == 0)
                {
                    PlayAudio(mediaPlayer2, "teemoLaugh1.mp3");
                }
                else if (randnum == laughChance && laughType == 1)
                {
                    PlayAudio(mediaPlayer2, "teemoLaugh2.mp3");
                }
                else if (randnum == laughChance && laughType == 2)
                {
                    PlayAudio(mediaPlayer2, "teemoLaugh3.mp3");
                }
                else
                    return;

                #endregion

            }
        }


        /// <summary>
        /// Randomly play Teemo's voicelines when trying to click him
        /// </summary>
        /// <param name="sender">A click of the mouse grid</param>
        /// <param name="e">The events of the mouse click</param>
        private void Teemo_Idle_Voice(object sender, MouseButtonEventArgs e)
        {
            // Variable numbers that create the randomization of voice lines
            // 33% chance of Teemo saying a voiceline
            int chance = 5;
            int randnum = rand.Next(15);
            int voicelineType = rand.Next(4);

            #region Voicelines logic

            // Teemo may laugh when you touch him
            if (randnum == chance && voicelineType == 0)
            {
                // Armed and Ready!
                PlayAudio(mediaPlayer3, "teemoidle1.mp3");
            }
            else if (randnum == chance && voicelineType == 1)
            {
                // Captain Teemo on duty!
                PlayAudio(mediaPlayer3, "teemoidle2.mp3");
            }
            else if (randnum == chance && voicelineType == 2)
            {
                // Hut, 2 3 4!
                PlayAudio(mediaPlayer3, "teemoidle3.mp3");
            }
            else if (randnum == chance && voicelineType == 3)
            {
                // Yes sir!
                PlayAudio(mediaPlayer3, "teemoidle4.mp3");
            }
            else
                return;

            #endregion

        }


        /// <summary>
        /// Handles the hover events of a Teemo shroom
        /// </summary>
        /// <param name="sender">The shroom that was hovered over</param>
        /// <param name="e">The events of the hover</param>
        private void Hover_Shroom(object sender, RoutedEventArgs e)
        {

            // Cast sender to a rectangle
            Rectangle rect = (Rectangle)sender;

            int row = Grid.GetColumn(rect);
            int col = Grid.GetRow(rect);

            //0 1 2 3 4... 34 35
            int index = row + (col * 13);

            // If you hover over a shroom, delete the shroom, place a new shroom in a random spot, and take damage.
            if (gridSpots[index] == CellType.Shroomed)
            {
                // Shroom explodes
                Explosion(rect);

                // Place a shroom elsewhere on the grid
                PlaceShroom(index);

                // Lose health
                TakeDamage();
            }

        }


        /// <summary>
        /// Plays the animation and sound of a shroom explosion
        /// </summary>
        private void Explosion(Rectangle rect)
        {
            // Expose the invisible teemo shroom
            rect.Opacity = 1;

            // The sound of the shroom exploding
            PlayAudio(mediaPlayer1, "shroomExplosion.mp3");

            // The shroom visual played on impact
            Storyboard sb1 = Resources["Shroom_Explosion"] as Storyboard;
            sb1.Begin(rect);

        }


        /// <summary>
        /// Randomly places a shroom on the grid in an unoccupied spot
        /// </summary>
        private void PlaceShroom(int index)
        {
            //The spot is clear of shrooms
            gridSpots[index] = CellType.Free;

            // Generate a random spot anywhere on the grid
            int r0 = rand.Next(0, 104);

            // Checks if the random spot has a shroom - if it does, find another number until it's free
            // Make sure you don't place a shroom where the cursor currently is
           while (index == r0 || gridSpots[r0] == CellType.Shroomed) {
                r0 = rand.Next(0, 104);
            }

            // The spot is free, so make it shroomed
            gridSpots[r0] = CellType.Shroomed;
            var cell = GridContainer.Children.OfType<Rectangle>().ToList().ElementAt(r0);
            cell.Fill = CreateShroom();
            cell.Opacity = 0.01;
        }


        /// <summary>
        /// Modifies the player's health after hovering over a shroom
        /// </summary>
        private async void TakeDamage()
        {
            // Player takes damage; decrease their health
            health -= 198;
            Health_Text.Content = health + " / 2375";

            #region Health bar updates

            // Updates the health bar depending on how much health the player has left
            if (health >= 2175)
            {
                // Play animation of health bar decreasing
                Storyboard sb1 = Resources["Health_Bar_Decrease1"] as Storyboard;
                sb1.Begin(Health_Bar_Front);
            }
            else if (health >= 1979)
            {
                Storyboard sb2 = Resources["Health_Bar_Decrease2"] as Storyboard;
                sb2.Begin(Health_Bar_Front);
            }
            else if (health >= 1781)
            {
                Storyboard sb3 = Resources["Health_Bar_Decrease3"] as Storyboard;
                sb3.Begin(Health_Bar_Front);
            }
            else if (health >= 1583)
            {
                Storyboard sb4 = Resources["Health_Bar_Decrease4"] as Storyboard;
                sb4.Begin(Health_Bar_Front);
            }
            else if (health >= 1385)
            {
                Storyboard sb5 = Resources["Health_Bar_Decrease5"] as Storyboard;
                sb5.Begin(Health_Bar_Front);
            }
            else if (health >= 1187)
            {
                Storyboard sb6 = Resources["Health_Bar_Decrease6"] as Storyboard;
                sb6.Begin(Health_Bar_Front);
            }
            else if (health >= 989)
            {
                Storyboard sb7 = Resources["Health_Bar_Decrease7"] as Storyboard;
                sb7.Begin(Health_Bar_Front);
            }
            else if (health >= 791)
            {
                Storyboard sb8 = Resources["Health_Bar_Decrease8"] as Storyboard;
                sb8.Begin(Health_Bar_Front);
            }
            else if (health >= 593)
            {
                Storyboard sb9 = Resources["Health_Bar_Decrease9"] as Storyboard;
                sb9.Begin(Health_Bar_Front);
            }
            else if (health >= 395)
            {
                Storyboard sb10 = Resources["Health_Bar_Decrease10"] as Storyboard;
                sb10.Begin(Health_Bar_Front);
            }
            else if (health >= 197)
            {
                Storyboard sb11 = Resources["Health_Bar_Decrease11"] as Storyboard;
                sb11.Begin(Health_Bar_Front);
            }
            // Hit the last shroom
            // Health decreases past 0 and the game ends
            else if (health <= 0)
            {
                health = 0;
                Health_Text.Content = 0 + " / 2375";
                Storyboard sb12 = Resources["Health_Bar_Decrease12"] as Storyboard;
                sb12.Begin(Health_Bar_Front);

            }
            else
                return;

            #endregion

            // Check if dead
            if (health <= 0)
            {
                // Teemo fades away...
                Storyboard sb13 = Resources["Teemo_Fadeaway"] as Storyboard;
                sb13.Begin(Teemo);

                await Task.Delay(3000);

                // Wipe the screen in preparation for Teemo's face animation
                ClearGame();
            }

        }


        /// <summary>
        /// Clears the screen of all entities - shrooms, health bar and Teemo
        /// </summary>
        private void ClearGame()
        {
            // Clear all shrooms off the grid
            for (int i = 0; i < gridSpots.Length; i++)
                gridSpots[i] = CellType.Free;

            // Iterate every rectangle on the grid to clear
            GridContainer.Children.OfType<Rectangle>().ToList().ForEach(rect =>
            {
                // Restore/reset background
                rect.Fill = Brushes.White;
            });

            // Make Teemo temporarily invisible
            Teemo.Visibility = Visibility.Collapsed;

            // Make the health bar temporarily invisible
            Health_Bar_Front.Visibility = Visibility.Collapsed;
            Health_Bar_Back.Visibility = Visibility.Collapsed;
            Health_Text.Visibility = Visibility.Collapsed;

            // Make the mana bar temporarily invisible
            Mana_Bar_Back.Visibility = Visibility.Collapsed;
            Mana_Bar_Front.Visibility = Visibility.Collapsed;
            Mana_Text.Visibility = Visibility.Collapsed;

            // Devil Teemo fades into the screen (very small)
            Storyboard sb1 = Resources["Teemo_Expand"] as Storyboard;
            sb1.Begin(Devil_Teemo);

            // Begin expansion
            PlayDevilTeemo();
            
        }


        /// <summary>
        /// Plays the Teemo expansion animation
        /// </summary>
        private async void PlayDevilTeemo()
        {
            // Set the background to fire!
            this.Background = new ImageBrush(new BitmapImage(
                new Uri(new Uri(dir + "\\Images\\firebg.jpg").AbsoluteUri)));

            // Make Devil Teemo visible
            Devil_Teemo.Fill = CreateDevilTeemoButton();
            Devil_Teemo.Visibility = Visibility.Visible;
            Devil_Teemo.Opacity = 1;

            // Play Devil Teemo's expansion
            Storyboard sb1 = Resources["Teemo_Expand"] as Storyboard;
            sb1.Begin(Devil_Teemo);

            #region Devil Teemo expansion sounds

            await Task.Delay(1000);
            PlayAudio(mediaPlayer4, "teemoLaugh1.mp3");
            await Task.Delay(100);
            PlayAudio(mediaPlayer5, "teemoLaugh1.mp3");
            await Task.Delay(100);
            PlayAudio(mediaPlayer6, "teemoLaugh1.mp3");

            await Task.Delay(500);
            PlayAudio(mediaPlayer4, "teemoLaugh1.mp3");

            await Task.Delay(750);
            PlayAudio(mediaPlayer4, "teemoLaugh3.mp3");
            await Task.Delay(500);
            PlayAudio(mediaPlayer5, "teemoLaugh2.mp3");

            await Task.Delay(500);
            PlayAudio(mediaPlayer4, "teemoLaugh1.mp3");

            // The long soundtrack uses mediaPlayer7
            await Task.Delay(500);
            PlayAudio(mediaPlayer7, "devilteemolaugh2.mp3");
            await Task.Delay(100);
            PlayAudio(mediaPlayer2, "devilteemogiggle3.wav");
            await Task.Delay(100);
            PlayAudio(mediaPlayer3, "teemoLaugh1.mp3");

            await Task.Delay(1000);
            PlayAudio(mediaPlayer4, "devilteemogiggle1.wav");
            await Task.Delay(500);
            PlayAudio(mediaPlayer5, "devilteemolaugh2.mp3");

            await Task.Delay(1000);
            PlayAudio(mediaPlayer4, "devilteemogiggle3.wav");
            await Task.Delay(250);
            PlayAudio(mediaPlayer5, "devilteemogiggle1.wav");
            await Task.Delay(500);
            PlayAudio(mediaPlayer6, "devilteemogiggle3.wav");

            await Task.Delay(250);
            PlayAudio(mediaPlayer2, "devilteemolaugh3.mp3");
            await Task.Delay(250);
            PlayAudio(mediaPlayer3, "devilteemolaugh3.mp3");
            await Task.Delay(750);
            PlayAudio(mediaPlayer4, "devilteemogiggle4.wav");
            await Task.Delay(250);
            PlayAudio(mediaPlayer5, "devilteemogiggle4.wav");
            await Task.Delay(400);
            PlayAudio(mediaPlayer6, "devilteemogiggle4.wav");

            await Task.Delay(100);
            PlayAudio(mediaPlayer2, "devilteemolaugh3.mp3");
            await Task.Delay(500);
            PlayAudio(mediaPlayer3, "devilteemolaugh4.mp3");

            // Plays "you have been slain" after devil teemo finishes
            await Task.Delay(7000);
            PlayAudio(mediaPlayer1, "youhavebeenslain.mp3");

            #endregion
        }


    }

}
