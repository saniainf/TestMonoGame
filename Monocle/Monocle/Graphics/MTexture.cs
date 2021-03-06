﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Monocle
{
	public class MTexture
	{
		private Dictionary<string, MTexture> atlas;

		public MTexture(string imagePath)
		{
			ImagePath = imagePath;
			AtlasPath = null;
			atlas = null;

#if DEBUG
			if (Engine.Instance.GraphicsDevice == null)
				throw new Exception("Cannot load until GraphicsDevice has been initialized");
			if (!File.Exists(Path.Combine(Engine.Instance.Content.RootDirectory, ImagePath)))
				throw new FileNotFoundException("Texture file does not exist: " + Path.Combine(Engine.Instance.Content.RootDirectory, ImagePath));
#endif
			FileStream stream = new FileStream(Path.Combine(Engine.Instance.Content.RootDirectory, ImagePath), FileMode.Open);
			Texture2D = Texture2D.FromStream(Engine.Instance.GraphicsDevice, stream);
			stream.Close();

			ClipRect = new Rectangle(0, 0, Texture2D.Width, Texture2D.Height);
			DrawOffset = Vector2.Zero;
			Width = ClipRect.Width;
			Height = ClipRect.Height;
		}

		public MTexture(int width, int height, Color color)
		{
			ImagePath = null;
			AtlasPath = null;
			atlas = null;

			Texture2D = new Texture2D(Engine.Instance.GraphicsDevice, width, height);
			var data = new Color[width * height];
			for (int i = 0; i < data.Length; i++)
				data[i] = color;
			Texture2D.SetData<Color>(data);

			ClipRect = new Rectangle(0, 0, width, height);
			DrawOffset = Vector2.Zero;
			Width = width;
			Height = height;
		}

		public MTexture(MTexture parent, int x, int y, int width, int height)
		{
			Texture2D = parent.Texture2D;
			ImagePath = parent.ImagePath;
			AtlasPath = null;
			atlas = null;

			ClipRect = parent.GetRelativeRect(x, y, width, height);
			DrawOffset = new Vector2(-Math.Min(x - parent.DrawOffset.X, 0), -Math.Min(y - parent.DrawOffset.Y, 0));
			Width = width;
			Height = height;
		}

		public MTexture(MTexture parent, Rectangle clipRect)
			: this(parent, clipRect.X, clipRect.Y, clipRect.Width, clipRect.Height)
		{

		}

		private MTexture(MTexture parent, string atlasPath, Rectangle clipRect, Vector2 drawOffset, int width, int height)
		{
			Texture2D = parent.Texture2D;
			ImagePath = parent.ImagePath;
			AtlasPath = atlasPath;
			atlas = null;

			ClipRect = parent.GetRelativeRect(clipRect);
			DrawOffset = drawOffset;
			Width = width;
			Height = height;
		}

		private MTexture(MTexture parent, string atlasPath, Rectangle clipRect)
			: this(parent, clipRect)
		{
			AtlasPath = atlasPath;
		}

		public void Unload()
		{
			Texture2D.Dispose();
			Texture2D = null;
			atlas = null;
		}

		public MTexture GetSubtexture(int x, int y, int width, int height)
		{
			return new MTexture(this, x, y, width, height);
		}

		public MTexture GetSubtexture(Rectangle rect)
		{
			return new MTexture(this, rect);
		}

		#region Properties

		public Texture2D Texture2D
		{
			get; private set;
		}

		public Rectangle ClipRect
		{
			get; private set;
		}

		public string ImagePath
		{
			get; private set;
		}

		public string AtlasPath
		{
			get; private set;
		}

		public Vector2 DrawOffset
		{
			get; private set;
		}

		public int Width
		{
			get; private set;
		}

		public int Height
		{
			get; private set;
		}

		#endregion

		#region Atlas

		public MTexture this[string id]
		{
			get
			{
				if (atlas == null)
					return null;
				else
					return atlas[id];
			}

			set
			{
				if (atlas == null)
					atlas = new Dictionary<string, MTexture>(StringComparer.InvariantCultureIgnoreCase);
				atlas[id] = value;
			}
		}
		public bool Has(string id)
		{
			return atlas.ContainsKey(id);
		}

		public List<MTexture> GetAtlasSubtextures(string key)
		{
			List<MTexture> list = new List<MTexture>();

			if (atlas != null)
				foreach (var kv in atlas)
					if (kv.Key.StartsWith(key))
						list.Add(kv.Value);

			return list;
		}

		public List<MTexture> GetAtlasSubtexturesNumbered(string key)
		{
			List<MTexture> list = new List<MTexture>();

			int n;
			if (atlas != null)
				foreach (var kv in atlas)
				{
					if (kv.Key.Equals(key) || (kv.Key.StartsWith(key) && int.TryParse(kv.Key.Substring(key.Length), out n)))
						list.Add(kv.Value);
				}

			return list;
		}

		public enum AtlasDataFormat { TexturePacker_Sparrow };

		public void LoadAtlasData(string dataPath, AtlasDataFormat format)
		{
			switch (format)
			{
				case AtlasDataFormat.TexturePacker_Sparrow:
					{
						XmlDocument xml = Calc.LoadContentXML(dataPath);
						XmlElement at = xml["TextureAtlas"];
						var subtextures = at.GetElementsByTagName("SubTexture");

						atlas = new Dictionary<string, MTexture>(subtextures.Count, StringComparer.InvariantCultureIgnoreCase);
						foreach (XmlElement sub in subtextures)
						{
							var clipRect = sub.Rect();
							if (sub.HasAttr("frameX"))
								atlas.Add(sub.Attr("name"), new MTexture(this, sub.Attr("name"), clipRect, new Vector2(-sub.AttrInt("frameX"), -sub.AttrInt("frameY")), sub.AttrInt("frameWidth"), sub.AttrInt("frameHeight")));
							else
								atlas.Add(sub.Attr("name"), new MTexture(this, sub.Attr("name"), clipRect));
						}
					}
					break;

				default:
					throw new NotImplementedException();
			}
		}

		#endregion

		#region Helpers

		public override string ToString()
		{
			if (AtlasPath != null)
				return AtlasPath;
			else
				return ImagePath;
		}

		private Rectangle GetRelativeRect(Rectangle rect)
		{
			return GetRelativeRect(rect.X, rect.Y, rect.Width, rect.Height);
		}

		private Rectangle GetRelativeRect(int x, int y, int width, int height)
		{
			int atX = (int)(ClipRect.X - DrawOffset.X + x);
			int atY = (int)(ClipRect.Y - DrawOffset.Y + y);

			int rX = (int)MathHelper.Clamp(atX, ClipRect.Left, ClipRect.Right);
			int rY = (int)MathHelper.Clamp(atY, ClipRect.Top, ClipRect.Bottom);
			int rW = Math.Max(0, Math.Min(atX + width, ClipRect.Right) - rX);
			int rH = Math.Max(0, Math.Min(atY + height, ClipRect.Bottom) - rY);

			return new Rectangle(rX, rY, rW, rH);
		}

		public bool Loaded
		{
			get { return Texture2D != null && !Texture2D.IsDisposed; }
		}

		public Vector2 Center
		{
			get
			{
				return new Vector2(Width / 2f, Height / 2f);
			}
		}

		public int TotalPixels
		{
			get { return Width * Height; }
		}

		#endregion

		#region Draw

		public void Draw(Vector2 position)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, Color.White, 0, -DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void Draw(Vector2 position, Vector2 origin)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, Color.White, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void Draw(Vector2 position, Vector2 origin, Color color)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void Draw(Vector2 position, Vector2 origin, Color color, float scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void Draw(Vector2 position, Vector2 origin, Color color, float scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void Draw(Vector2 position, Vector2 origin, Color color, float scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, origin - DrawOffset, scale, flip, 0);
		}

		public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, origin - DrawOffset, scale, flip, 0);
		}

		public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation, Rectangle clip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, GetRelativeRect(clip), color, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
		}

		#endregion

		#region Draw Centered

		public void DrawCentered(Vector2 position)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, Color.White, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void DrawCentered(Vector2 position, Color color)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void DrawCentered(Vector2 position, Color color, float scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawCentered(Vector2 position, Color color, float scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawCentered(Vector2 position, Color color, float scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, Center - DrawOffset, scale, flip, 0);
		}

		public void DrawCentered(Vector2 position, Color color, Vector2 scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawCentered(Vector2 position, Color color, Vector2 scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawCentered(Vector2 position, Color color, Vector2 scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, Center - DrawOffset, scale, flip, 0);
		}

		#endregion

		#region Draw Justified

		public void DrawJustified(Vector2 position, Vector2 justify)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, Color.White, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void DrawJustified(Vector2 position, Vector2 justify, Color color)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void DrawJustified(Vector2 position, Vector2 justify, Color color, float scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);
		}

		public void DrawJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif
			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);
		}

		#endregion

		#region Draw Outline

		public void DrawOutline(Vector2 position)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, -DrawOffset, 1f, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, Color.White, 0, -DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void DrawOutline(Vector2 position, Vector2 origin)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, Color.White, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void DrawOutline(Vector2 position, Vector2 origin, Color color)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void DrawOutline(Vector2 position, Vector2 origin, Color color, float scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutline(Vector2 position, Vector2 origin, Color color, float scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutline(Vector2 position, Vector2 origin, Color color, float scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, origin - DrawOffset, scale, flip, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, origin - DrawOffset, scale, flip, 0);
		}

		public void DrawOutline(Vector2 position, Vector2 origin, Color color, Vector2 scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, origin - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutline(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, origin - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutline(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, origin - DrawOffset, scale, flip, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, origin - DrawOffset, scale, flip, 0);
		}

		#endregion

		#region Draw Outline Centered

		public void DrawOutlineCentered(Vector2 position)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, Color.White, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void DrawOutlineCentered(Vector2 position, Color color)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void DrawOutlineCentered(Vector2 position, Color color, float scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutlineCentered(Vector2 position, Color color, float scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutlineCentered(Vector2 position, Color color, float scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, Center - DrawOffset, scale, flip, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, Center - DrawOffset, scale, flip, 0);
		}

		public void DrawOutlineCentered(Vector2 position, Color color, Vector2 scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, Center - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutlineCentered(Vector2 position, Color color, Vector2 scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, Center - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutlineCentered(Vector2 position, Color color, Vector2 scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, Center - DrawOffset, scale, flip, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, Center - DrawOffset, scale, flip, 0);
		}

		#endregion

		#region Draw Outline Justified

		public void DrawOutlineJustified(Vector2 position, Vector2 justify)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, Color.White, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
		}

		public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, float scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);
		}

		public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, 0, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale, float rotation)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
		}

		public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale, float rotation, SpriteEffects flip)
		{
#if DEBUG
			if (Texture2D.IsDisposed)
				throw new Exception("Texture2D Is Disposed");
#endif

			for (var i = -1; i <= 1; i++)
				for (var j = -1; j <= 1; j++)
					if (i != 0 || j != 0)
						Monocle.Draw.SpriteBatch.Draw(Texture2D, position + new Vector2(i, j), ClipRect, Color.Black, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);

			Monocle.Draw.SpriteBatch.Draw(Texture2D, position, ClipRect, color, rotation, new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);
		}

		#endregion
	}
}
