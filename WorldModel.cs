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
			const double ResistanceCoefficient = 0.005;

			double posX = Cursor.Position.X;
			double posY = Cursor.Position.Y;

			DoResist(ResistanceCoefficient);

			velY += GravitationForce;

			BallX = MoveOnAxis(BallX, velX, distX, WorldWidth);
			BallY = MoveOnAxis(BallY, velY, distY, WorldHeight);

			MoveByCursor(posX, posY);
			 
		}
		public double MoveOnAxis(double ball, double vel, double dist, double world)
        {
			/*if (velX > 0) BallX = Math.Min(BallX + distX, WorldWidth - BallRadius);
			else BallX = Math.Max(BallX + distX, BallRadius);

			if (BallX + BallRadius == WorldWidth) velX *= -1;
			else if (BallX - BallRadius == 0) velX *= -1;

			return velX;
			*/
			if (vel > 0) ball = Math.Min(ball + dist, world - BallRadius);
			else ball = Math.Max(ball + dist, BallRadius);

			if (ball + BallRadius == world) vel *= -1;
			else if (ball - BallRadius == 0) vel *= -1;

			if (world == WorldWidth) velX = vel;
			else velY = vel;

			return ball;
		}

		public void DoResist(int resistance)
        {
			double resistY = velY * resistance;
			double resistX = velX * resistance;

			velY -= resistY;
			velX -= resistX;
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