using Semi_Photoshop.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Semi_Photoshop
{
    public partial class Form1 : Form
    {
        #region Private Members

        /// <summary>
        /// The choosen image and the manipulated image through the lifetime of the application
        /// </summary>
        private Bitmap choosenImage;

        /// <summary>
        /// List of already loaded images that are present
        /// </summary>
        private List<CustomImage> loadedImages;

        /// <summary>
        /// Amount of brightness to apply for the image
        /// </summary>
        private float brightness = 1;

        /// <summary>
        /// History of the editions on the main image
        /// Used to get back to the last edition
        /// </summary>
        private Stack<Bitmap> history;

        /// <summary>
        /// Indicator for the start crop button has been pressed
        /// </summary>
        private bool isCropping = false;

        /// <summary>
        /// Indicator for after starting to crop, the mouse has been pressed down on the main image
        /// </summary>
        private bool croppingStarted = false;

        /// <summary>
        /// Indicator for notifying that the copy button has been clicked
        /// </summary>
        private bool isCoping = false;

        /// <summary>
        /// Indicator for notifying that mouse has been pressed on the main image
        /// after copy button has been clicked
        /// </summary>
        private bool copyStarted = false;

        /// <summary>
        /// Indicator used to notify that a draw shape button has been clicked
        /// </summary>
        private bool isDrawing = false;

        /// <summary>
        /// Indicator used to notify that mouse has been pressed down on the main image
        /// after notifying that draw shape button has been pressed
        /// </summary>
        private bool drawingStarted = false;

        /// <summary>
        /// Indicator to notify that the paste button has been pressed
        /// </summary>
        private bool isPasting = false;

        /// <summary>
        /// Indicator to notify that mouse has been clicked on the image 
        /// after paste button has been clicked
        /// </summary>
        private bool pasteStarted = false;

        /// <summary>
        /// Shape currently being drawn
        /// </summary>
        private DrawingShape drawingShape;

        /// <summary>
        /// Color being used for drawing the shapes
        /// </summary>
        private Color color;

        /// <summary>
        /// Coordinated that are captured once the mouse is firstly pressed on the main image after indicating or drawing
        /// </summary>
        private int cX, cY;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //Register the lostfocus event of the limit textbox
            this.txtLimit.LostFocus += onLimitTextBoxLostFocus;

            this.Width *= 2;
            this.Height = (int)(this.Height * 1.6);

            this.drawingShape = DrawingShape.None;
            this.color = Color.White;

            this.history = new Stack<Bitmap>();
            this.loadedImages = new List<CustomImage>();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Opens a dialog for the user to choose an image to work with
        /// </summary>
        /// <param name="sender">Source of event</param>
        /// <param name="e">Additional info of the event</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create an open file dialog instance
            var ofd = new OpenFileDialog
            {
                //Initialize the title to an apporpriate text
                Title = "Choose an image",
                //Filter to the supported image formats
                Filter = "All Images|*.jpg; *.jpeg; *.png; *.bmp"
            };

            //Open the dialog and check if the user has choose an image
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var customImage = new CustomImage { ImageId = Guid.NewGuid().ToString(), Image = new Bitmap(ofd.FileName), Name = new FileInfo(ofd.FileName).Name };
                customImage.Image = new Bitmap(customImage.Image);
                this.choosenImage = customImage.Image;
                this.loadedImages.Add(customImage);

                this.mainPicture.Image = customImage.Image;

                this.lstImages1.Items.Add(new ListViewItem(customImage.Name));
                //Reset the history
                this.history.Clear();
            }
        }

        /// <summary>
        /// Prints the basic information of the loaded image
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Additional event information</param>
        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if an image has been selected already
            if (this.choosenImage == null)
            {
                return;
            }

            //Get a summary info of the choosen image
            var summaryInfo = "Image Width: " + this.choosenImage.Width + Environment.NewLine
                            + "Image Height: " + this.choosenImage.Height + Environment.NewLine
                            + "Horizontal Resolution: " + this.choosenImage.HorizontalResolution + Environment.NewLine
                            + "Vertical Resolution: " + this.choosenImage.VerticalResolution + Environment.NewLine
                            + "Pixel Format: " + this.choosenImage.PixelFormat;

            //Output the information to the screen
            MessageBox.Show(summaryInfo,
                            "Image Information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        /// <summary>
        /// Handles when the mouse moves over the picture
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">Addition information about the event</param>
        private void onMouseMove(object sender, MouseEventArgs e)
        {
            //Check if there is an image choosen
            if (choosenImage == null)
            {
                return;
            }

            //Mouse is in boundaries of the image
            if (e.X <= this.choosenImage.Width && e.Y <= this.choosenImage.Height)
            {
                //Get the RGB color of the pixel the mouse is at
                var color = (this.mainPicture.Image as Bitmap).GetPixel(e.X, e.Y);

                //Get the color of the pixel that the mouse is on and give it to the picture element
                this.colorPicture.BackColor = color;

                //Get the hslColor
                var hslColor = Semi_Photoshop.Models.ColorConverter.FromRGBToHSL(color);

                //Set the values of hsl color to the ui
                this.hColor.Text = hslColor.H.ToString();
                this.sColor.Text = hslColor.S.ToString();
                this.lColor.Text = hslColor.L.ToString();
            }

            #region Cropping

            //If we have chose to crop and pressed the mouse on the image
            if (croppingStarted)
            {
                //Get appropriate coordinates
                int x1 = Math.Min(cX, e.X);
                int y1 = Math.Min(cY, e.Y);
                int x2 = Math.Max(cX, e.X);
                int y2 = Math.Max(cY, e.Y);

                using (Graphics g = this.mainPicture.CreateGraphics())
                {
                    this.mainPicture.Refresh();

                    //Draw cropping rectangle
                    g.DrawRectangle(new Pen(this.color), x1, y1, x2 - x1, y2 - y1);
                }
            }

            #endregion

            #region Drawing Shape

            //If we have choose to draw a particular shape and pressed the mouse on the image
            if (this.drawingStarted)
            {
                //Get the appropriate coordinates
                int x1 = Math.Min(cX, e.X);
                int y1 = Math.Min(cY, e.Y);
                int x2 = Math.Max(cX, e.X);
                int y2 = Math.Max(cY, e.Y);

                using (Graphics g = this.mainPicture.CreateGraphics())
                {
                    var pen = new Pen(this.color);
                    this.mainPicture.Refresh();

                    //Check the shape
                    switch (this.drawingShape)
                    {
                        //Draw a line
                        case DrawingShape.Line:
                            g.DrawLine(pen, cX, cY, e.X, e.Y);
                            break;
                        //Draw a circle
                        case DrawingShape.Circle:
                            g.DrawEllipse(pen, x1, y1, x2 - x1, y2 - y1);
                            break;
                        //Draw a rectangle
                        case DrawingShape.Rectangle:
                            g.DrawRectangle(pen, x1, y1, x2 - x1, y2 - y1);
                            break;
                        //Default case
                        case DrawingShape.None:
                            break;
                    }
                }
            }

            #endregion

            #region Copying

            if (copyStarted)
            {
                //Get the coordinates
                int x1 = Math.Min(cX, e.X);
                int y1 = Math.Min(cY, e.Y);
                int x2 = Math.Max(cX, e.X);
                int y2 = Math.Max(cY, e.Y);

                using (Graphics graphics = this.mainPicture.CreateGraphics())
                {
                    //Refresh the main image for performance issues
                    this.mainPicture.Refresh();

                    //Draw a rectangle of copying
                    graphics.DrawRectangle(Pens.Black, x1, y1, x2 - x1, y2 - y1);
                }
            }

            #endregion

            #region Pasting

            if (pasteStarted)
            {
                //Get the appropriate coordinates
                int x1 = Math.Min(cX, e.X);
                int y1 = Math.Min(cY, e.Y);
                int x2 = Math.Max(cX, e.X);
                int y2 = Math.Max(cY, e.Y);

                using (Graphics g = this.mainPicture.CreateGraphics())
                {
                    this.mainPicture.Refresh();

                    g.DrawImage(ImageContainer.CopiedImage, x1, y1, x2 - x1, y2 - y1);
                }
            }

            #endregion
        }

        /// <summary>
        /// Handles the event of scaling the loaded image to gray
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if there has been an image actually loaded
            if (this.choosenImage == null)
            {
                return;
            }

            //Save last changes
            this.history.Push(this.choosenImage);

            // Get the gray version of the image
            this.choosenImage = scaleImageColor(PixelColor.Gray);

            //Load it as a main image
            this.mainPicture.Image = this.choosenImage;
        }

        /// <summary>
        /// Handles the event of scaling the loaded image to green
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if there has been an image actually loaded
            if (this.choosenImage == null)
            {
                return;
            }

            //Save last changes
            this.history.Push(this.choosenImage);

            //Get the green scaled version of the image
            this.choosenImage = scaleImageColor(PixelColor.Green);

            //Set it as a main image
            this.mainPicture.Image = this.choosenImage;
        }

        /// <summary>
        /// Handles the event of scaling the loaded image to red
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if there has been an image actually loaded
            if (this.choosenImage == null)
            {
                return;
            }

            //Save last changes
            this.history.Push(this.choosenImage);

            //Get the red scaled version of the image
            this.choosenImage = scaleImageColor(PixelColor.Red);

            //Set the scaled image as a main image
            this.mainPicture.Image = this.choosenImage;
        }

        /// <summary>
        /// Handles the event of scaling the loaded image to blue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if there has been an image actually loaded
            if (this.choosenImage == null)
            {
                return;
            }

            //Save last changes
            this.history.Push(this.choosenImage);

            //Get the blue scaled version of the image
            this.choosenImage = scaleImageColor(PixelColor.Blue);

            //Set the scaled image as the main image
            this.mainPicture.Image = this.choosenImage;
        }

        /// <summary>
        /// Brightens the main picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrighten_Click(object sender, EventArgs e)
        {
            //Check if there has been an image actually loaded
            if (this.choosenImage == null)
            {
                return;
            }

            //Save last changes
            this.history.Push(this.choosenImage);

            //Manipulate the brightness of the image
            this.choosenImage = adjustBrightness(true, 1.1f);

            //Set the adjusted image as the main image
            this.mainPicture.Image = this.choosenImage;
        }

        /// <summary>
        /// Darkens the main picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDarken_Click(object sender, EventArgs e)
        {
            //Check if there has been an image actually loaded
            if (this.choosenImage == null)
            {
                return;
            }

            //Save last changes
            this.history.Push(this.choosenImage);

            //Manipulate the brightness of the image
            this.choosenImage = adjustBrightness(false, 0.9f);

            //Set the adjusted image as the main image
            this.mainPicture.Image = this.choosenImage;
        }

        /// <summary>
        /// Handles when the limit text box loses focus
        /// </summary>
        /// <param name="sender">Limit textbox</param>
        /// <param name="e">Additional info about the event</param>
        private void onLimitTextBoxLostFocus(object sender, EventArgs e)
        {
            //Check if there has been an image actually loaded
            if (this.choosenImage == null)
            {
                return;
            }

            //Regular expression to evaluate
            var regex = "^[0-9]+$";

            //Disable the limit textbox
            this.txtLimit.Enabled = false;

            //Check if the limit text is empty of spaces (apply on effect)
            if (string.IsNullOrEmpty(this.txtLimit.Text) || string.IsNullOrWhiteSpace(this.txtLimit.Text))
            {
                this.txtLimit.Enabled = true;
                return;
            }

            //Evaluate the expression
            if (!Regex.IsMatch(this.txtLimit.Text, regex))
            {
                MessageBox.Show("Invalid Input.", "Error");
                this.txtLimit.Enabled = true;
                return;
            }

            //Cast the limit text to an integer
            var limit = Convert.ToByte(this.txtLimit.Text);

            //Check if limit is less than zero
            if (limit < 0)
            {
                MessageBox.Show("Limit can't be less than zero!", "Error");
                this.txtLimit.Enabled = true;
                return;
            }

            //Save last changes
            this.history.Push(this.choosenImage);

            //Get the limited version of the image
            this.choosenImage = getLimitedImage(limit);

            //Set the limited image as the main image
            this.mainPicture.Image = this.choosenImage;

            //Re enable the limit textbox 
            this.txtLimit.Enabled = true;
        }

        /// <summary>
        /// Handles scaling the main image to gray using the marshal version
        /// </summary>
        /// <param name="sender">Item clicked</param>
        /// <param name="e">Additional event information</param>
        private void toBetterGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if there has been an image loaded or not
            if (this.choosenImage == null)
            {
                return;
            }

            //Save last changes
            this.history.Push(this.choosenImage);

            this.choosenImage = toBetterGrayCustom(this.choosenImage);

            this.mainPicture.Image = this.choosenImage;
        }

        /// <summary>
        /// Handles adding an additional color layer that the user selects
        /// </summary>
        /// <param name="sender">Menu item that was clicked</param>
        /// <param name="e">Additional event information</param>
        private void addColorLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.choosenImage == null)
                return;

            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            var selectedColor = colorDialog.Color;

            //Save last changes
            this.history.Push(this.choosenImage);

            for (int i = 0; i < this.choosenImage.Width; i++)
            {
                for (int j = 0; j < this.choosenImage.Height; j++)
                {
                    //Get the color of the current pixel
                    var currentColor = this.choosenImage.GetPixel(i, j);


                    byte nr = (byte)((currentColor.R + selectedColor.R) / 2);
                    byte ng = (byte)((currentColor.G + selectedColor.G) / 2);
                    byte nb = (byte)((currentColor.B + selectedColor.B) / 2);

                    this.choosenImage.SetPixel(i, j, Color.FromArgb(nr, ng, nb));

                }
            }

            this.mainPicture.Image = this.choosenImage;
        }

        /// <summary>
        /// Handles clicking the blend image menu item
        /// </summary>
        /// <param name="sender">Menu item that was clicked</param>
        /// <param name="e">Additional event information</param>
        private void blendImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if there is a loaded image
            if (this.choosenImage == null)
            {
                return;
            }

            //Make open file dialog to get an image from the user
            var ofd = new OpenFileDialog
            {
                Title = "Choose an image",
                Filter = "All Images|*.jpg;*.png;*.jpeg;*.bmp"
            };

            //Prompts the user to choosen an image
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Load the choosen image
                Bitmap tempImage = new Bitmap(ofd.FileName);

                //Make it 32 bits
                tempImage = new Bitmap(tempImage);

                //Save last changes
                this.history.Push(this.choosenImage);

                //Blend the choosen image with the current image and set it to the current iamge
                this.choosenImage = blendImages(tempImage, this.choosenImage);

                //Set the main image as the choosen blended image
                this.mainPicture.Image = this.choosenImage;
            }
        }

        /// <summary>
        /// Handles clicking the save item
        /// </summary>
        /// <param name="sender">Menu item</param>
        /// <param name="e">Additional data about the event</param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if an image has already been loaded
            if (this.choosenImage == null)
                return;

            //Create a save dialog
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "JPG file|*.jpg"
            };

            //Prompt user to give a filename for the image
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            //Save the image to the specified path
            saveCurrentImage(sfd.FileName, 20L);
        }

        /// <summary>
        /// Hangles clicking the start button at the rotate menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rotateStartClicked(object sender, EventArgs e)
        {
            //Validate the current image
            if (this.choosenImage == null)
            {
                return;
            }

            int angle;
            if (int.TryParse(this.txtAngle.Text, out angle))
            {
                //Save last changes
                this.history.Push(this.choosenImage);
                this.choosenImage = rotate(this.choosenImage, angle);
            }
            else
                MessageBox.Show("Incorrect angle", "Error");

            this.mainPicture.Image = this.choosenImage;

        }

        /// <summary>
        /// Undoes the last changes done to the image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if there is any saved image in the stack
            if (this.history.Count > 0)
            {
                //Retrieve the last change
                this.choosenImage = this.history.Pop();
                this.mainPicture.Image = this.choosenImage;
            }
        }

        /// <summary>
        /// Handles Clicking the crop button in the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Validate the current image
            if (this.choosenImage == null)
            {
                return;
            }

            //Indicate that the crop button has been clicked
            isCropping = true;

            //Make the cursor cross for editing effects
            this.mainPicture.Cursor = Cursors.Cross;

            //Get the color from the user
            this.color = askForColor();
        }

        /// <summary>
        /// Handles when the mouse is clicked on the main image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainPictureMouseDown(object sender, MouseEventArgs e)
        {
            if (isCropping)
            {
                //Indicate that mouse has been clicked while we are cropping
                croppingStarted = true;

                //Get the initial coordinated of the shape to be drawn
                cX = e.X;
                cY = e.Y;
            }

            if (isDrawing)
            {
                //Indicate that mouse has been clicked while we are drawing
                drawingStarted = true;

                //Get the initial coordinated of the shape to be drawn
                cX = e.X;
                cY = e.Y;
            }

            if (isCoping)
            {
                //Indicate that we started copying
                this.copyStarted = true;

                //Get the initial coordinates
                this.cX = e.X;
                this.cY = e.Y;
            }

            if (isPasting)
            {
                //Indicate that mouse has been clicked while we are pasting
                this.pasteStarted = true;

                //Get the initial coordinated of the shape to be drawn
                this.cX = e.X;
                this.cY = e.Y;
            }
        }

        /// <summary>
        /// Handles when the mouse is released on the main image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainPictureMouseUp(object sender, MouseEventArgs e)
        {
            //if cropping has already been started and we already did press on the image before
            if (croppingStarted)
            {
                //Rest indicators
                croppingStarted = false;
                isCropping = false;

                //Reset cursor
                this.mainPicture.Cursor = Cursors.Default;
                this.mainPicture.Image = this.choosenImage;

                //Get max and min coordinates
                int x1 = Math.Min(cX, e.X);
                int y1 = Math.Min(cY, e.Y);
                int x2 = Math.Max(cX, e.X);
                int y2 = Math.Max(cY, e.Y);

                //Push lates changes
                this.history.Push(this.choosenImage);

                //Crop the current image with the piece specified and make it the current image
                this.choosenImage = crop(this.choosenImage, x1, y1, x2 - x1, y2 - y1);
                this.mainPicture.Image = this.choosenImage;
            }

            //if drawing has already been started and we already did press on the image before
            if (drawingStarted)
            {
                //Rest indicators
                isDrawing = false;
                drawingStarted = false;

                //Reset cursor
                this.mainPicture.Cursor = Cursors.Default;
                this.mainPicture.Image = this.choosenImage;

                //Save last changes
                this.history.Push(this.choosenImage);
                var customImage = this.loadedImages.FirstOrDefault(image => image.Image == this.choosenImage);
                //Draw the shape on the image
                this.choosenImage = drawShape(this.choosenImage, this.drawingShape, this.color, cX, cY, e.X, e.Y);
                customImage.Image = this.choosenImage;
                this.mainPicture.Image = this.choosenImage;
                this.drawingShape = DrawingShape.None;
            }

            if (copyStarted)
            {
                //Reset indicators
                this.isCoping = false;
                this.copyStarted = false;
                this.mainPicture.Cursor = Cursors.Default;

                //Get coordinates
                int x1 = Math.Min(cX, e.X);
                int y1 = Math.Min(cY, e.Y);
                int x2 = Math.Max(cX, e.X);
                int y2 = Math.Max(cY, e.Y);

                //Set the global copied image to the selected part of the current image
                ImageContainer.CopiedImage = copyImageSelected(this.choosenImage, x1, y1, x2 - x1, y2 - y1);

                //Show a success message that the part selected was copied
                MessageBox.Show("Selected Part has been copied.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Set the main image to the current
                this.mainPicture.Image = this.choosenImage;
            }

            if (pasteStarted)
            {
                //Rest indicators
                isPasting = false;
                pasteStarted = false;

                //Reset cursor
                this.mainPicture.Cursor = Cursors.Default;
                this.mainPicture.Image = this.choosenImage;

                //Get max and min coordinates
                int x1 = Math.Min(cX, e.X);
                int y1 = Math.Min(cY, e.Y);
                int x2 = Math.Max(cX, e.X);
                int y2 = Math.Max(cY, e.Y);

                //Save last changes
                this.history.Push(this.choosenImage);

                //Pasted the copied part on the current image
                this.choosenImage = pasteImage(this.choosenImage, ImageContainer.CopiedImage, x1, y1, x2 - x1, y2 - y1);
                this.loadedImages.FirstOrDefault(i => i.Name == this.lstImages1.FocusedItem.Text).Image = new Bitmap(this.choosenImage);
                this.mainPicture.Image = this.choosenImage;
            }
        }

        /// <summary>
        /// Handles Clicking the start button at the merge menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mergeStartClicked(object sender, EventArgs e)
        {
            //Validate the current image
            if (this.choosenImage == null)
            {
                return;
            }

            //Validate the mode
            if (this.cbMergeMode.Text != "Horizontal" && this.cbMergeMode.Text != "Vertical")
            {
                MessageBox.Show("Merge mode should be selected before merging.", "Error");
                return;
            }

            //Get the mode
            var mode = this.cbMergeMode.Text == "Horizontal" ? MergeMode.Horizontal : MergeMode.Vertical;

            //Get the image to merge
            var ofd = new OpenFileDialog
            {
                Title = "Choose an image",
                Filter = "Images|*.jpeg;*.jpg;*.png;*.bmp"
            };

            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;

            //Get the image to be merged
            var imageToMerge = new Bitmap(ofd.FileName);

            //Save last changes
            this.history.Push(this.mainPicture.Image as Bitmap);

            //Merge images
            this.choosenImage = merge(this.choosenImage, imageToMerge, mode);
            this.mainPicture.Image = this.choosenImage;
        }

        /// <summary>
        /// Hangles clicking on the line button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawLineClicked(object sender, EventArgs e)
        {
            //Validate the current image
            if (this.choosenImage == null)
            {
                return;
            }

            //Save last changes
            this.history.Push(this.choosenImage);

            //Set the currently drawing shape to a line
            drawingDefaults(DrawingShape.Line);
        }

        /// <summary>
        /// Hangles clicking on the circle button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawCircleClicked(object sender, EventArgs e)
        {
            //Validate the current image
            if (this.choosenImage == null)
            {
                return;
            }

            //Save last changes
            this.history.Push(this.choosenImage);

            //Set the currently drawing shape to a line
            drawingDefaults(DrawingShape.Circle);
        }

        /// <summary>
        /// Hangles clicking on the rectangle button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawRectangleClicked(object sender, EventArgs e)
        {
            //Validate the current image
            if (this.choosenImage == null)
            {
                return;
            }

            //Save last changes
            this.history.Push(this.choosenImage);

            //Set the currently drawing shape to a line
            drawingDefaults(DrawingShape.Rectangle);
        }

        /// <summary>
        /// Handles when Do Flip button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onDoFlipClicked(object sender, EventArgs e)
        {
            //Validate the current image
            if (this.choosenImage == null)
            {
                return;
            }

            //Get mode text
            var modeText = this.cbFlipMode.Text;

            //Validate mode
            if (modeText != "Horizontal" && modeText != "Vertical")
            {
                MessageBox.Show("Flipping mode must be selected.", "Error");
                return;
            }

            //Get the appropriate mode type
            var flippingMode = modeText == "Horizontal" ? FlippingMode.Horizontal : FlippingMode.Vertical;

            //Save Changes
            this.history.Push(this.choosenImage);

            //Flip the image
            this.choosenImage = flip(this.choosenImage, flippingMode);
            this.mainPicture.Image = this.choosenImage;
        }

        /// <summary>
        /// Handles when the paste button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onPasteClicked(object sender, EventArgs e)
        {
            //Validate the current image
            if (this.choosenImage == null)
            {
                return;
            }

            //Check if there has an image been copied
            if (ImageContainer.CopiedImage == null)
            {
                MessageBox.Show("No image has been copied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Indicate that the paste button has been pressed
            this.isPasting = true;

            //Change cursor for editing effects
            this.mainPicture.Cursor = Cursors.Cross;
        }

        /// <summary>
        /// Handles clicking the copy button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onCopyClicked(object sender, EventArgs e)
        {
            //Indicate that we are copying
            this.isCoping = true;

            //Set the cursor of the main image for editing effects
            this.mainPicture.Cursor = Cursors.Cross;
        }

        /// <summary>
        /// Handles Selecting an image from the already loaded images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstImages1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = this.lstImages1.FocusedItem.Text;
            try
            {
                if (this.loadedImages.Count == 0)
                {
                    this.mainPicture.Image = null;
                    this.choosenImage = null;
                }

                else
                {
                    this.choosenImage = this.loadedImages.FirstOrDefault(image => image.Name.Equals(selected)).Image;
                    this.mainPicture.Image = this.choosenImage;
                }
            }
            catch { }
        }

        /// <summary>
        /// Handles double clicking on an image from the list of pre loaded images
        /// Basically deletes an image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onListImageDoubleClick(object sender, MouseEventArgs e)
        {
            var selected = this.lstImages1.FocusedItem.Text;
            var selectedImage = this.loadedImages.FirstOrDefault(image => image.Name.Equals(selected));
            int index = this.loadedImages.IndexOf(selectedImage);
            this.loadedImages.Remove(selectedImage);
            this.lstImages1.Items.Remove(this.lstImages1.FocusedItem);
            this.choosenImage = null;
            this.mainPicture.Image = null;
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Limits an image to a certain limit
        /// </summary>
        /// <param name="limit">Limit to use</param>
        /// <returns></returns>
        private Bitmap getLimitedImage(byte limit)
        {
            //Result image
            var tempImage = new Bitmap(this.choosenImage.Width, this.choosenImage.Height);

            //Loop over every pixel
            for (int i = 0; i < tempImage.Width; i++)
            {
                for (int j = 0; j < tempImage.Height; j++)
                {
                    //Get the current color for the pixel
                    var currentColor = this.choosenImage.GetPixel(i, j);

                    //Check its RGB values according to the limit passed
                    var R = currentColor.R >= limit ? currentColor.R : 0;
                    var G = currentColor.G >= limit ? currentColor.G : 0;
                    var B = currentColor.B >= limit ? currentColor.B : 0;

                    //Create a new color according to the processed RGB values
                    var newColor = Color.FromArgb(R, G, B);

                    //Set the color of the current pixel
                    tempImage.SetPixel(i, j, newColor);
                }
            }

            //Return the result image
            return tempImage;
        }

        /// <summary>
        /// Adjusts the brighness of the passed image
        /// </summary>
        /// <param name="t">Image to adjust</param>
        /// <param name="brightness">Brightness value to use for adjusting</param>
        /// <returns></returns>
        private Bitmap adjustBrightness(bool brighten, float brightness)
        {
            //Create the result image
            Bitmap bm = new Bitmap(this.choosenImage.Width, this.choosenImage.Height);

                float b = brightness;

                // Make the ColorMatrix.
                ColorMatrix cm = new ColorMatrix(new float[][]
                    {
                        new float[] {b, 0, 0, 0, 0},
                        new float[] {0, b, 0, 0, 0},
                        new float[] {0, 0, b, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1},
                    });

                ImageAttributes attributes = new ImageAttributes();

                attributes.SetColorMatrix(cm);

                // Draw the image onto the new bitmap while applying
                // the new ColorMatrix.
                Point[] points =
                {
                new Point(0, 0),
                new Point(this.choosenImage.Width, 0),
                new Point(0, this.choosenImage.Height),
            };
                Rectangle rect = new Rectangle(0, 0, this.choosenImage.Width, this.choosenImage.Height);

                using (Graphics gr = Graphics.FromImage(bm))
                {
                    gr.DrawImage(this.choosenImage, points, rect,
                        GraphicsUnit.Pixel, attributes);
                }

            // Return the result.
            return bm;
        }

        /// <summary>
        /// Scales the loaded image to the specified pixel color
        /// </summary>
        /// <param name="color">Color to scale to</param>
        /// <param name="userCustomAlgorithm">Indicates whether to use the faster version algorithm</param>
        /// <returns></returns>
        private Bitmap scaleImageColor(PixelColor color)
        {
            //Create the resulting image
            Bitmap bm = new Bitmap(this.choosenImage.Width, this.choosenImage.Height);

            //get a graphics object from the new image
            using (Graphics g = Graphics.FromImage(bm))
            {
                //Create the grayscale ColorMatrix
                ColorMatrix colorMatrix = null;

                //Check the color passed to the method
                switch (color)
                {
                    case PixelColor.Unknown:
                        break;
                    case PixelColor.Gray:
                        //Set the color matrix to its equivalent values
                        colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                        new float[] {.3f, .3f, .3f, 0, 0},
                        new float[] {.59f, .59f, .59f, 0, 0},
                        new float[] {.11f, .11f, .11f, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                   });
                        break;
                    case PixelColor.Red:
                        //Set the color matrix to its equivalent values
                        colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                        new float[] {1, 0, 0, 0, 0},
                        new float[] {0, 0, 0, 0, 0},
                        new float[] {0, 0, 0, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                   });
                        break;
                    case PixelColor.Green:
                        //Set the color matrix to its equivalent values
                        colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                        new float[] {0, 0, 0, 0, 0},
                        new float[] {0, 1, 0, 0, 0},
                        new float[] {0, 0, 0, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                   });
                        break;
                    case PixelColor.Blue:
                        //Set the color matrix to its equivalent values
                        colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                        new float[] {0, 0, 0, 0, 0},
                        new float[] {0, 0, 0, 0, 0},
                        new float[] {0, 0, 1, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                   });
                        break;
                    default:
                        break;
                }

                //create some image attributes
                ImageAttributes attributes = new ImageAttributes();

                //set the color matrix attribute
                attributes.SetColorMatrix(colorMatrix);

                //draw the original image on the new image
                //using the grayscale color matrix
                g.DrawImage(choosenImage, new Rectangle(0, 0, choosenImage.Width, choosenImage.Height),
                   0, 0, choosenImage.Width, choosenImage.Height, GraphicsUnit.Pixel, attributes);
            }

            //Return the resulting processed image
            return bm;
        }

        /// <summary>
        /// Gets the bytes of the of an image
        /// </summary>
        /// <param name="image">Image to get bytes of</param>
        /// <returns></returns>
        private byte[] getImageBytes(Bitmap image)
        {
            var imageLock = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                image.PixelFormat);

            var result = new byte[imageLock.Height * imageLock.Stride];

            Marshal.Copy(imageLock.Scan0, result, 0, result.Length);
            image.UnlockBits(imageLock);

            return result;
        }

        /// <summary>
        /// Copies the bytes passed to the image passed
        /// </summary>
        /// <param name="image">Image to copy to the bytes to</param>
        /// <param name="data">Data to set for the image</param>
        private void setBytesOfImage(Bitmap image, byte[] data)
        {
            var imageLock = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                 ImageLockMode.WriteOnly, image.PixelFormat);

            Marshal.Copy(data, 0, imageLock.Scan0, data.Length);

            image.UnlockBits(imageLock);
        }

        /// <summary>
        /// Scales the image to gray using marshal copy
        /// </summary>
        /// <param name="image">Image to scale</param>
        private Bitmap toBetterGrayCustom(Bitmap image)
        {
            //Get the bytes of the image
            var data = getImageBytes(image);

            for (int i = 0; i < data.Length; i += 4)
            {
                //Get the b component
                byte b = data[i];
                //Get the g component
                byte g = data[i + 1];
                //Get the r component
                byte r = data[i + 2];

                //Find the max of the components
                int max = Math.Max(b, Math.Max(g, r));
                //Find the min of the components
                int min = Math.Min(b, Math.Min(g, r));

                //Get the average of the max and min
                byte value = (byte)((max + min) / 2);
                //Set the current pixel components to the average
                data[i] = data[i + 1] = data[i + 2] = value;
            }

            //Set the bytes of the image
            setBytesOfImage(image, data);

            return new Bitmap(image);
        }

        /// <summary>
        /// Blending two images together
        /// </summary>
        /// <param name="image1">First image to blend</param>
        /// <param name="image2">Second image to blend</param>
        /// <returns></returns>
        private Bitmap blendImages(Bitmap image1, Bitmap image2)
        {
            //Get the max dimensions from the two images
            var maxWidth = Math.Max(image1.Width, image2.Width);
            var maxHeight = Math.Max(image1.Height, image2.Height);

            //Create the result image
            Bitmap resultImage;

            //Get the bytes of the images
            var image1Bytes = getImageBytes(image1);
            var image2Bytes = getImageBytes(image2);

            //Create the resulting byte array
            var resultBytes = new byte[maxWidth * maxHeight * 4];

            //Loop using the maximum width and height
            for (int i = 0; i < maxHeight; i++)
            {
                for (int j = 0; j < maxWidth; j++)
                {
                    //Bytes to be placed in the resulting byte array
                    byte pR = 0, pG = 0, pB = 0;

                    //Indicator for whether we have took first image's pixel into account
                    bool found = false;

                    //if the current row and column is in the boundries of the first image
                    if (i < image1.Height && j < image1.Width)
                    {
                        found = true;
                        pR = image1Bytes[i * image1.Width * 4 + j * 4 + 2];
                        pG = image1Bytes[i * image1.Width * 4 + j * 4 + 1];
                        pB = image1Bytes[i * image1.Width * 4 + j * 4];
                    }

                    //if the current row and column is in the boundries of the second image
                    if (i < image2.Height && j < image2.Width)
                    {
                        //if first image was taken into account
                        if (found)
                        {
                            //Get the RGB components from the second image's bytes
                            byte ppr = image2Bytes[i * image2.Width * 4 + j * 4 + 2];
                            byte ppg = image2Bytes[i * image2.Width * 4 + j * 4 + 1];
                            byte ppb = image2Bytes[i * image2.Width * 4 + j * 4];

                            //Put into the resulting RGB components the average of RGBs
                            pR = (byte)((pR + ppr) / 2);
                            pB = (byte)((pB + ppg) / 2);
                            pG = (byte)((pG + ppb) / 2);
                        }
                        //First image wasn't taken into account
                        else
                        {
                            //Put into the resulting RGB components the bytes of the 
                            //corresponding bytes of the second image
                            pR = image2Bytes[i * image2.Width * 4 + j * 4 + 2];
                            pG = image2Bytes[i * image2.Width * 4 + j * 4 + 1];
                            pB = image2Bytes[i * image2.Width * 4 + j * 4];
                        }
                    }

                    //Put in the resulting bytes array the calculated RGB components
                    resultBytes[i * maxWidth * 4 + j * 4] = pB;
                    resultBytes[i * maxWidth * 4 + j * 4 + 1] = pG;
                    resultBytes[i * maxWidth * 4 + j * 4 + 2] = pR;
                    resultBytes[i * maxWidth * 4 + j * 4 + 3] = 255;
                }
            }

            //Initialize the resutling image with the max width and height
            resultImage = new Bitmap(maxWidth, maxHeight, image1.PixelFormat);

            //Set the bytes of the resulting image to the resulting bytes array
            setBytesOfImage(resultImage, resultBytes);

            //Return the resulting image
            return resultImage;
        }

        /// <summary>
        /// Saves the current image to the specified path wit the specified quality
        /// </summary>
        /// <param name="path">Path to save the image to</param>
        /// <param name="quality">Quality to use while saving the image</param>
        /// <returns></returns>
        private bool saveCurrentImage(string path, long quality)
        {
            try
            {
                //Get the codec suitable for jpeg files
                var codec = ImageCodecInfo.GetImageEncoders().Single(c => c.MimeType == "image/jpeg");

                //Create the parameter object for the quality with its value
                EncoderParameter parameter = new EncoderParameter(Encoder.Quality, quality);

                //Create a parameters collection
                EncoderParameters param = new EncoderParameters(1);

                //Save the quality parameter to the parameters collection
                param.Param[0] = parameter;

                //Save the current image using the codec and the parameters collection
                this.choosenImage.Save(path, codec, param);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Rotates the passed image in a specific angle
        /// </summary>
        /// <param name="angle">Angle to use to rotating the image</param>
        /// <returns></returns>
        private Bitmap rotate(Bitmap image, int angle)
        {
            var radian = angle * Math.PI / 180;
            int w = Math.Abs((int)(Math.Cos(radian) * image.Width + Math.Sin(radian) * image.Height));
            int h = Math.Abs((int)(Math.Sin(radian) * image.Width + Math.Cos(radian) * image.Height));
            Bitmap newB = new Bitmap(w, h, image.PixelFormat);

            using (Graphics graphics = Graphics.FromImage(newB))
            {
                graphics.Clear(Color.Black);
                graphics.TranslateTransform(newB.Width / 2, newB.Height / 2);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-image.Width / 2, -image.Height / 2);
                graphics.DrawImage(image, 0, 0);
                //graphics.DrawLine(new Pen(Brushes.Red, 10), new Point(0, this.choosenImage.Height / 2), new Point(this.choosenImage.Width, this.choosenImage.Height / 2));
            }

            return newB;
        }

        /// <summary>
        /// Crops the image passed from the specified coordinated to a particular height and width
        /// </summary>
        /// <param name="image">Image to crop</param>
        /// <param name="x">The x coordinate to start from</param>
        /// <param name="y">The y coordinate to start from</param>
        /// <param name="width">Width to crop</param>
        /// <param name="height">Height to crop</param>
        /// <returns>The cropped piece of the passed image</returns>
        private Bitmap crop(Bitmap image, int x, int y, int width, int height)
        {
            //Create the result image
            Bitmap newB = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(newB))
            {
                g.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            }

            return newB;
        }

        /// <summary>
        /// Merges two images that are passed according to a mode specified
        /// </summary>
        /// <param name="image1">First image to merge</param>
        /// <param name="image2">Second image to merge</param>
        /// <param name="mode">Mode to use for merging</param>
        /// <returns></returns>
        private Bitmap merge(Bitmap image1, Bitmap image2, MergeMode mode)
        {
            //Create the resulting image
            Bitmap resultImage = null;

            //Check the mode choosen
            switch (mode)
            {
                case MergeMode.Horizontal:
                    {
                        //Get widths combined
                        int width = image1.Width + image2.Width;
                        //Get highest height
                        int height = Math.Max(image1.Height, image2.Height);

                        //Assign the resulting image with the appropriate width and height
                        resultImage = new Bitmap(width, height);

                        //Use graphics for drawing
                        using (Graphics graphics = Graphics.FromImage(resultImage))
                        {
                            //Make  the background black
                            graphics.Clear(Color.Black);

                            //Draw first image
                            graphics.DrawImage(image1, 0, 0);
                            //Draw second image beside it
                            graphics.DrawImage(image2, image1.Width, 0, 
                                image1.Width > image2.Width ? image2.Width : image1.Width, 
                                image1.Height > image2.Height ? image1.Height : image2.Height);
                        }

                        break;
                    }

                case MergeMode.Vertical:
                    {
                        //Get highest width
                        int width = Math.Max(image1.Width, image2.Width);
                        //Get heights combined
                        int height = image1.Height + image2.Height;

                        //Assign the resulting image with the appropriate width and height
                        resultImage = new Bitmap(width, height);

                        //Use graphics for drawing
                        using (Graphics graphics = Graphics.FromImage(resultImage))
                        {
                            //Make  the background black
                            graphics.Clear(Color.Black);
                            //Draw first image
                            graphics.DrawImage(image1, 0, 0);
                            //Draw second image belows it
                            graphics.DrawImage(image2, 0, image1.Height,
                                image1.Width > image2.Width ? image1.Width : image2.Width,
                                image1.Height > image2.Height ? image2.Height : image1.Height);
                        }

                        break;
                    }
            }

            //Return the resulting image
            return resultImage;
        }

        /// <summary>
        /// Sets the default settings for drawing mode
        /// </summary>
        /// <param name="shape">Shape to be drawn</param>
        private void drawingDefaults(DrawingShape shape)
        {
            //Set the currently drawing shape to a line
            this.drawingShape = shape;

            //Indicate that a shape button has been clicked
            this.isDrawing = true;

            //Make the cursor cross for editing effects
            this.mainPicture.Cursor = Cursors.Cross;

            //Get a color from the user
            this.color = askForColor();
        }

        /// <summary>
        /// Draws a shape on the image passed
        /// </summary>
        /// <param name="image">Image to draw the shape on</param>
        /// <param name="shape">Shape to draw</param>
        /// <param name="initialX">The initial x coordinate of the shape</param>
        /// <param name="initialY">The initial y coordinate of the shape</param>
        /// <param name="destinationX">Width of the shape</param>
        /// <param name="destinationY">Height of the shape</param>
        /// <returns></returns>
        private Bitmap drawShape(Bitmap image, DrawingShape shape, Color color, int initialX, int initialY, int destinationX, int destinationY)
        {
            //Create resulting image
            Bitmap resultImage = new Bitmap(image);

            //Get the appropriate coordinates
            int x1 = Math.Min(initialX, destinationX);
            int y1 = Math.Min(initialY, destinationY);
            int x2 = Math.Max(initialX, destinationX);
            int y2 = Math.Max(initialY, destinationY);

            using (Graphics g = Graphics.FromImage(resultImage))
            {
                var pen = new Pen(color);

                //Check shape to draw
                switch (shape)
                {
                    //Draw a line
                    case DrawingShape.Line:
                        g.DrawLine(pen, initialX, initialY, destinationX, destinationY);
                        break;
                    //Draw a Circle
                    case DrawingShape.Circle:
                        g.DrawEllipse(pen, x1, y1, x2 - x1, y2 - y1);
                        break;
                    //Draw a Rectangle
                    case DrawingShape.Rectangle:
                        g.DrawRectangle(pen, x1, y1, x2 - x1, y2 - y1);
                        break;
                    //Default case
                    case DrawingShape.None:
                        break;
                }
            }

            //Return the resulting image
            return resultImage;
        }

        /// <summary>
        /// Gets a color from the user
        /// </summary>
        /// <returns></returns>
        private Color askForColor()
        {
            var cd = new ColorDialog();

            return cd.ShowDialog() == DialogResult.OK ? cd.Color : Color.Empty;
        }

        /// <summary>
        /// Mirrors an image according to x or y axis
        /// </summary>
        /// <param name="imageToFlip">Image to be flipped</param>
        /// <param name="mode">Mode to use for flipping</param>
        /// <returns></returns>
        private Bitmap flip(Bitmap imageToFlip, FlippingMode mode)
        {
            Bitmap resultImage = new Bitmap(imageToFlip);

            switch (mode)
            {
                case FlippingMode.Horizontal:
                    resultImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case FlippingMode.Vertical:
                    resultImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;
                default:
                    break;
            }

            return resultImage;
        }

        /// <summary>
        /// Pasted a copied image on the image passed
        /// </summary>
        /// <param name="image">Image to paste on</param>
        /// <param name="imageToPaste">Image to be pasted</param>
        /// <param name="x">Initial x coordinate</param>
        /// <param name="y">Initial y coordinate</param>
        /// <param name="width">Width to use for pasting</param>
        /// <param name="height">Height to use for pasting</param>
        /// <returns></returns>
        private Bitmap pasteImage(Bitmap image, Bitmap imageToPaste, int x, int y, int width, int height)
        {
            Bitmap resultingImage = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(resultingImage))
            {
                g.DrawImage(image, 0, 0, image.Width, image.Height);
                g.DrawImage(imageToPaste, x, y, width, height);
            }

            return resultingImage;
        }

        /// <summary>
        /// Copies the selected part from the current image
        /// </summary>
        /// <param name="image">Image to copy from</param>
        /// <param name="x">Initial x coordinate</param>
        /// <param name="y">Initial y coordinate</param>
        /// <param name="width">Width to use for copying</param>
        /// <param name="height">Height to use for copying</param>
        /// <returns></returns>
        private Bitmap copyImageSelected(Bitmap image, int x, int y, int width, int height)
        {
            //Create the resulting image
            Bitmap resultingImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(resultingImage))
            {
                //Draw the selected part from the original image to the resulting image
                g.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            }

            //Return the resulting image
            return resultingImage;
        }

        #endregion
    }
}
