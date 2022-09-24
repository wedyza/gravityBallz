using System;
using System.Windows.Forms;
namespace GravityBalls
{
	public class WorldModel
	{
		public double BallX;
		public double BallY;
		public double BallRadius;
		public double WorldWidth;
		public double WorldHeight;
		public double velY = 200;
	    public double velX = 200;
		public MouseButtons button;

		public void SimulateTimeframe(double dt)
		{
			double distY = velY * dt, distX = velX * dt;

			const double GravitationForce = 10;

			double posX = Cursor.Position.X;
			double posY = Cursor.Position.Y;

			double resistY = velY * 0.005;
			double resistX = velX * 0.005;

			velY -= resistY;
			velX -= resistX;
			velY += GravitationForce;

			velX = MoveOnXAxis(velX, distX);
			velY = MoveOnYAxis(velY, distY);

			MoveByCursor(posX, posY);
			 
		}

		public double MoveOnXAxis(double velX, double distX)
        {
			if (velX > 0) BallX = Math.Min(BallX + distX, WorldWidth - BallRadius);
			else BallX = Math.Max(BallX + distX, BallRadius);

			if (BallX + BallRadius == WorldWidth) velX *= -1;
			else if (BallX - BallRadius == 0) velX *= -1;

			return velX;
		}

		public double MoveOnYAxis(double velY, double distY)
        {
			if (velY > 0) BallY = Math.Min(BallY + distY, WorldHeight - 1 - BallRadius);
			else BallY = Math.Max(BallY + distY, BallRadius);

			if (BallY + BallRadius == WorldHeight - 1) velY *= -1;
			else if (BallY - BallRadius == 0) velY *= -1;

			return velY;
		}
		
		public void MoveByCursor(double posX, double posY)
        {
			const double CursorPower = 400;

			double distX = posX - BallX;
			double distY = posY - BallY;
			double dist = Math.Sqrt(distX * distX + distY * distY);
			double sinAngle = distY / dist;
			double cosAngle = distX / dist;
			dist = CursorPower / dist;

			double newDistX = dist * cosAngle;
			double newDistY = dist * sinAngle;

			velY -= newDistY;
			velX -= newDistX;
        }

		public void BallIsTeleporting(double posX, double posY)
        {
			BallX = posX;
			BallY = posY;
			velY = 0;
			velX = 0;
			return;
        }

		public bool ButtonClick(MouseEventArgs e)
        {
			button = e.Button;
			if (button == MouseButtons.Left) return true;
			return false;
        }
	}
}