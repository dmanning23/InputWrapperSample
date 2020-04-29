using FilenameBuddy;
using FontBuddyLib;
using GameTimer;
using HadoukInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

		private InputState m_Input = new InputState();
		private InputWrapper _inputWrapper;

		List<string> moves;

		#endregion //Members

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			moves = new List<string>();

			Mappings.UseKeyboard[0] = true;
			_inputWrapper = new InputWrapper(new ControllerWrapper(0), _clock.GetCurrentTime);
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			_text.LoadContent(Content, "ArialBlack14");
			using (var content = new ContentManager(Services) { RootDirectory = "Content" })
			{
				_inputWrapper.ReadXmlFile(new Filename("GrimoireMoveList.xml"), content);
			}

			_clock.Start();
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

			//update all the input, then check if it found anything
			_clock.Update(gameTime);
			m_Input.Update();
			_inputWrapper.Update(m_Input, false);

			//If any patterns were matched in the input, they will be returned ni the NextMove method
			var nextMove = _inputWrapper.GetNextMove();

			if (!string.IsNullOrEmpty(nextMove))
			{
				moves.Add(nextMove);
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

			spriteBatch.Begin();

			Vector2 position = Vector2.Zero;

			//say what controller we are checking
			var lineSpacing = _text.MeasureString("Input Buffer: ");
			_text.Write("Input Buffer: " + _inputWrapper.GetBufferedInput(), position, Justify.Left, 1.0f, Color.White, spriteBatch, _clock);
			position.Y += lineSpacing.Y;

			_text.Write("Input Queue: " + _inputWrapper.ToString(), position, Justify.Left, 1.0f, Color.White, spriteBatch, _clock);
			position.Y += lineSpacing.Y;

			_text.Write("Current Move: ", position, Justify.Left, 1.0f, Color.White, spriteBatch, _clock);
			position.Y += lineSpacing.Y;

			for (int i = moves.Count - 1; i >= 0; i--)
			{
				_text.Write(moves[i], position, Justify.Left, 0.5f, Color.White, spriteBatch, _clock);
				position.Y += lineSpacing.Y * 0.5f;
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
