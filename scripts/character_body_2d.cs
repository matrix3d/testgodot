using Godot;
using System;

public partial class character_body_2d : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public override void _Ready(){
		GD.Print("This is Godot.");
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		AnimatedSprite2D myAnimatedSprite2D = GetNode<AnimatedSprite2D>("myAnimatedSprite2D");
			
		if (direction.X!=0){
			myAnimatedSprite2D.FlipH=direction.X<0;
			
			
			GD.Print( myAnimatedSprite2D+""+(direction.X>0?1:-1));
		}
		if (IsOnFloor()){
			if (direction.X == 0)
				myAnimatedSprite2D.Play("idle");
			else
				myAnimatedSprite2D.Play("run");
		}
		else{
			myAnimatedSprite2D.Play("jump");
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
