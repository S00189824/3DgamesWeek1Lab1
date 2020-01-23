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

        //How is triangle transformed
        Matrix ColourWorld = Matrix.Identity;

        //Camera
        Matrix view;//whrere are we and whrer are we looking
        Matrix Projection;// FOV ,near and far plane,aspect ratio

        //texture verts
        VertexPositionTexture[] textureVertices;
        BasicEffect textureEffesct;
        Matrix textureWorld = Matrix.Identity * Matrix.CreateTranslation(-1, 0, 0);
        Texture2D texture;

        //Color Vertices


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
            UpdateView();
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(90),
                GraphicsDevice.DisplayMode.AspectRatio,
                0.1f,
                1000);

            base.Initialize();
        }

        void UpdateView()
        {
            view = Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, -1), Vector3.Up);
        }

        void CreateColorVertices()
        {
            colorVertives = new VertexPositionColor[3];

            //instantiate the 3 vertices -> position and color
            colorVertives[0] = new VertexPositionColor(new Vector3(1,0,0),Color.ForestGreen);
            colorVertives[1] = new VertexPositionColor(new Vector3(-1,0,0),Color.BlanchedAlmond);
            colorVertives[2] = new VertexPositionColor(new Vector3(0,1,0),Color.Violet);

            colorEffect = new BasicEffect(GraphicsDevice);
            colorEffect.VertexColorEnabled = true;
        }

        void CreateTextureVertices()
        {
            texture = Content.Load<Texture2D>("uv_texture");
            //create an array
            textureVertices = new VertexPositionTexture[3];

            //instaniate the 3 vertices -> position and color
            textureVertices[0] = new VertexPositionTexture(new Vector3(1, 0, 0),new Vector2(1,1));
            textureVertices[1] = new VertexPositionTexture(new Vector3(-1, 0, 0),new Vector2(0,1));
            textureVertices[2] = new VertexPositionTexture(new Vector3(0, 1, 0), new Vector2(0.5f,0.5f));

            textureEffesct = new BasicEffect(GraphicsDevice);
            textureEffesct.TextureEnabled = true;
            textureEffesct.Texture = texture;
        }
       
        protected override void LoadContent()
        {
            CreateColorVertices();
            CreateTextureVertices();
        }

        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateView();
            textureWorld *= Matrix.CreateRotationY(MathHelper.ToRadians(5.9f));
            textureWorld *= Matrix.CreateRotationX(MathHelper.ToRadians(5.5f));
            textureWorld *= Matrix.CreateRotationZ(MathHelper.ToRadians(5.5f));
            base.Update(gameTime);
        }

        private void DrawColorTriangle()
        {
            //pass data from C# to GPU (HLSL)
            colorEffect.World = ColourWorld;
            colorEffect.View = view;
            colorEffect.Projection = Projection;

            //foreach pass (Method) in the shader
            foreach (EffectPass pass in colorEffect.CurrentTechnique.Passes)
            {
                //apply the pass to the vertices (call the method)
                pass.Apply();

                //send the data
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, colorVertives, 0, colorVertives.Length / 3);
            }
        }

        private void DrawVert()
        {
            //pass data from C# to GPU (HLSL)
            textureEffesct.World = textureWorld;
            textureEffesct.View = view;
            textureEffesct.Projection = Projection;

            foreach(EffectPass pass in textureEffesct.CurrentTechnique.Passes)
            {
                pass.Apply();

                 GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, textureVertices, 0, textureVertices.Length / 3);
            }
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawColorTriangle();
            DrawVert();
            base.Draw(gameTime);
        }
    }
}
