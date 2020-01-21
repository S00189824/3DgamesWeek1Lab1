using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DWeek1Lab1
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        //array to store triangle verts
        //Vertex positioncolor-> Position (XYZ),Color(RGB)
        VertexPositionColor[] colorVertives;

        //shader to used to render the vertices
        BasicEffect colorEffect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            IsMouseVisible = true;



            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            

            base.Initialize();
        }

        void CreateColorVertices()
        {
            colorVertives = new VertexPositionColor[3];

            //instantiate the 3 vertices -> position and color
            colorVertives[0] = new VertexPositionColor(new Vector3(1,0,0),Color.Red);
            colorVertives[1] = new VertexPositionColor(new Vector3(-1,0,0),Color.Green);
            colorVertives[2] = new VertexPositionColor(new Vector3(0,1,0),Color.Blue);

            colorEffect = new BasicEffect(GraphicsDevice);colorEffect.VertexColorEnabled = true;
        }
       
        protected override void LoadContent()
        {
            CreateColorVertices();
        }

        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            

            base.Draw(gameTime);
        }
    }
}
