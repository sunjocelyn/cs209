using System;

public class Sprite {
 	private int XCoor;
 	private int YCoor;
 	private int ZCoor;
	private uint Horiz;
	private uint Vert;
 	private uint Health;
	private uint Shield;

	/* Default constructor
	 * set all values to 0
	 */
 	public Sprite()
 	{
		XCoor = 0;
		YCoor = 0;
		ZCoor = 0;
		Horiz = 0;
		Vert = 0;
		Health = 100;
		Shield = 0;
	}

	/* SetState
  	 * receives inputs for all instance variables.
	 * sets all of the instance variables to inputs
	 */
	public void SetState(int x, int y, int z, 
		uint horiz, uint vert, uint h, uint s)
	{
		XCoor = x;
		YCoor = y;
		ZCoor = z;
		Horiz = horiz;
		Vert = vert;
		Health = h;
		Shield = s;
	}

	/* Equals
	 * Compares an input object to the current object
	 * If the input object is non-null, a Sprite, and all 
	 * fields match, return true, otherwise return false
	 */
	public override bool Equals(Object obj)
	{
		//Check for null and compare run-time types.
      		if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
      		{
         		return false;
      		}
      		else 
		{
         		Sprite s = (Sprite) obj;
			if ((XCoor == s.XCoor) &&
				(YCoor == s.YCoor) &&
				(ZCoor == s.ZCoor) &&
				(Horiz == s.Horiz) &&
				(Vert == s.Vert) &&
				(Health == s.Health) &&
				(Shield == s.Shield))
			{
				return true;
			}
			else
			{
				return false;
			}
      		}
	}

	/* GetHashCode
	 * This produces a non-unique but at least somewhat unique
	 * number based on a sprite's state.
	 */
	public override int GetHashCode()
	{
		return (int)(XCoor ^ (YCoor << 8) ^ (ZCoor << 16) ^
			Horiz ^ (Vert << 9) ^ 
			(Health << 18) ^ (Shield << 25));
	}

	/* ToString
	 * Creates a string that can be used to print out the 
	 * Sprite's state in human-readable form
	 */
	public override String ToString()
	{
		return String.Format(
			"Sprite: loc({0},{1},{2}),rot({3},{4}),hs({5},{6})",
			XCoor, YCoor, ZCoor, Horiz, Vert, Health, Shield);
	}

	/* HorizontalRotate
	 * rotates along the horizontal axis.
	 * inputs:
	 *   int degrees - amount to rotate
	 * outputs:
	 *   modifies instance variable Horiz
	 */
	public void HorizontalRotate(int degrees)
	{
        int degMax = 360;
        int degree = degrees % degMax;

        int newpos = (int)(this.Horiz + degree + degMax) % degMax;
        this.Horiz = (uint)newpos;
	}

	/* VerticalRotate
	 * rotates along the vertical axis.
	 * inputs:
	 *   int degrees - amount to rotate
	 * outputs:
	 *   modifies instance variable Vert
	 */
	public void VerticalRotate(int degrees)
	{
		int degMax = 360;
        int degree = degrees % degMax;

        int newpos = (int)(this.Vert + degree + degMax) % degMax; //adding degMax accounts for negative inputs
        this.Vert = (uint)newpos;
	}



	/* Move
 	 * Changes the location to move Sprite in direction it is pointing
	 * along the horizontal axis.
	 * inputs:
	 *   int steps
	 * outputs:
	 *   modifies instance variables XCoor, YCoor as appropriate
	 */
	public void Move(int steps)
	{
		int x = this.XCoor;
        int y = this.YCoor;
        double h = Math.Sqrt(x*x + y*y) + steps;
        double deg = this.Horiz;
        double rad = deg * (Math.PI / 180);

        this.YCoor = (int) Math.Round(h * Math.Sin(rad));
        this.XCoor = (int) Math.Round(h * Math.Cos(rad));
	}

	/* DrinkSmallShieldPot
	 * Increases Shield by 25, up to a max of 50
	 * outputs:
	 *  Modifies instance variable Shield
	 *  returns 1 if made modification, 0 if not
 	 */
	public uint DrinkSmallShieldPot()
	{
		if (this.Shield < 50){
            this.Shield += 25;

            if (this.Shield > 50){
                this.Shield = 50;
            }

            return 1;
        }
		return 0;
	}

	/* DrinkLargeShieldPot
	 * Increases Shield by 50, up to a max of 100
	 * outputs:
	 *  Modifies instance variable Shield
	 *  returns 1 if made modification, 0 if not
 	 */
	public uint DrinkLargeShieldPot()
	{
		if (this.Shield < 100){
            this.Shield += 50;

            if (this.Shield > 100){
                this.Shield = 100;
            }
            return 1;

        }
        return 0;
	}

	/* ApplyBandage
	 * Increases Health by 15, up to a max of 75
	 * outputs:
	 *  Modifies instance variable Health
	 *  returns 1 if made modification, 0 if not
 	 */
	public uint ApplyBandage()
	{
		if (this.Health < 75){
            if (this.Health + 15 > 75){
                this.Health = 75;
                return 1;
            } else {
                this.Health += 15;
                return 1;
            }
        }
		return 0;
	}

	/* UseMedKit
	 * Sets Health to 100
	 * outputs:
	 *  Modifies instance variable Health
	 *  returns 1 if made modification, 0 if not
 	 */
	public uint UseMedKit()
	{
		if (this.Health < 100){
            this.Health = 100;
            return 1;
        }
		return 0;
	}

	/* WeaponsDamage
	 * Decrements Shield by damage, down to a min of 0
	 * If still damage left, decrements Health by leftover damage,
	 * down to a min of 0
	 * outputs:
	 *  Modifies instance variables Shield, Health
	 *  returns 1 if resulting Health > 0, 0 if not
 	 */
	public uint WeaponDamage(uint damage)
	{
		if (this.Shield >= damage){
            this.Shield -= damage;

        } else {
            this.Shield = 0;
            uint rollover = damage - this.Shield;

            if (rollover > this.Health){
                this.Health = 0;
            } else {
                this.Health -= rollover;
            }
        }

        if (this.Health > 0){
            return 1;
        }
        //health can only go down if there are no shields

		return 0;
	}

	/*
	   DO NOT IMPLEMENT THIS FUNCTION
	 FallDamage
	  Decrements Health by damage, down to a min of 0
	  outputs:
	   Modifies instance variable Health
	   returns 1 if resulting Health > 0, 0 if not
 	 
	public uint FallDamage(uint damage)
	{
		Console.WriteLine("Not Yet Implemented");
		return 0;
	}
	*/

	/* Revive
	 * This is also applied only to a Sprite that is dead. 
	 * If the Sprite is dead, restores it to 30 health, 0 shield. 
	 * Return 1 if the person had been dead (and is now revived), 
	 * 0 if it was called on an ineligible Sprite (and did nothing).
	 */
	public uint Revive()
	{
        if (this.Health == 0 && this.Shield == 0){
            this.Health = 30;
            return 1;
        }
		return 0;
	}

	/* Reboot
	 * This is also applied only to a Sprite that is dead. 
	 * If the Sprite is dead, restores it to 100 health, 0 shield. 
	 * Return 1 if the person had been dead (and is now rebooted), 
	 * 0 if it was called on an ineligible Sprite (and did nothing).
	 */
	public uint Reboot()
	{
		if (this.Health == 0 && this.Shield == 0){
            this.Health = 100;
            return 1;
        }
		return 0;
	}


}