# What does the code do?
## This is a script to pick up objects in unity. It works by starting a coroutine that smoothly moves the object from the ground to your "hand" ('pos' varible), then the object keeps following that position.

> [!TIP]
> If you want to make it less smooth, just uncomment this section of the code on the Update void: `//rb.MovePosition = obj.transform.position;` and comment (or delete) the next line.

> [!IMPORTANT]
> Do not forget to apply a RigidBody to the object that you want to pick up and set it to the layer that receives the raycast.

I hope this helps you!
