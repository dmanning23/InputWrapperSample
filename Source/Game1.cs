using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FontBuddyLib;
using GameTimer;
using HadoukInput;
using FilenameBuddy;

namespace InputWrapperSample
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		#region Members

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		/// <summary>
		/// A font buddy we will use to write out to the screen
		/// </summary>
		private FontBuddy _text = new FontBuddy();

		/// <summary>
		/// A game clock for the input wrapper
		/// </summary>
		private GameClock _clock = new GameClock();

		private CountdownTimer _timer = new CountdownTimer();

		private InputState m_Input = new InputState();
		private InputWrapper _inputWrapper;
		private StateMachineWrapper _states = new StateMachineWrapper();

		/// <summary>
		/// used to write out text to the screen
		/// </summary>
		string _currentMove = "none";

		#endregion //Members

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			_inputWrapper = new InputWrapper(new ControllerWrapper(PlayerIndex.One, true), _clock.GetCurrentTime);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			_text.LoadContent(Content, "ArialBlack14");

			_inputWrapper.ReadXmlFile(new Filename("MoveList.xml"), _states.NameToIndex);

			_clock.Start();
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			// TODO: Add your update logic here

			//update all the input, then check if it found anything
			_clock.Update(gameTime);
			_timer.Update(_clock);
			m_Input.Update();
			_inputWrapper.Update(m_Input, false);

			//write out the buffer
			Vector2 position = Vector2.Zero;

			//If any patterns were matched in the input, they will be returned ni the NextMove method
			int iNextMove = _inputWrapper.GetNextMove();

			if (0.0f >= _timer.RemainingTime())
			{
				_currentMove = "none";
			}

			if ((-1 != iNextMove) && (0.0 >= _timer.RemainingTime()))
			{
				_timer.Start(0.5f);
				_currentMove = _states.MoveNames[iNextMove];
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			spriteBatch.Begin();

			Vector2 position = Vector2.Zero;

			//say what controller we are checking
			_text.Write("Input Buffer: " + _inputWrapper.GetBufferedInput(), position, Justify.Left, 1.0f, Color.White, spriteBatch);
			position.Y += _text.Font.MeasureString("test").Y;

			_text.Write("Input Queue: " + _inputWrapper.ToString(), position, Justify.Left, 1.0f, Color.White, spriteBatch);
			position.Y += _text.Font.MeasureString("test").Y;

			_text.Write("Current Move: " + _currentMove, position, Justify.Left, 1.0f, Color.White, spriteBatch);
			position.Y += _text.Font.MeasureString("test").Y;

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
