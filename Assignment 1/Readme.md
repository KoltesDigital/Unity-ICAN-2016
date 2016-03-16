# Assignment 1: driving game prototype

Deadline: **Sunday, March 20, 11:59 PM** CET for **2GD1**.

Prototype goals:

1. There are trees on the side of the road.
2. Trees are located only on the greenish part of the road texture.
3. Trees are randomly placed, yet they have a certain coherency. Think about a forest: trees are regularly spaced, not too close, not too far away from each other.
4. The car doesn't move vertically from the player's point of view.
5. The player controls the horizontal movement of the car (e.g. by pressing two keys).
6. Car movement is smoothed.
7. The car is not allowed to go off the greyish part of the road texture. If this happens, the car is teleported to the center of the road, and looses its speed.
8. Road signs appear at random intervals, not too close, not to far away from each other.
9. Signs are randomly chosen, but diversified: the same sign should not appear twice in a row.
10. Signs appear on the black part of the road texture, randomly on the left or on the right.
11. Road, trees, and signs move toward the player under the player's control (e.g. by pressing a key), in order to simulate the car driving.

Notes on assets:

* The road maps the whole texture horizontally, but (exactly) 25% of it vertically. Thus you can offset it to simulate the car driving.
* A script is already provided. Its purpose is to snap the objects to the road, provided an angle in radians.
* The road cylinder is cut to spare resources. Its revolution angle is about 120 degrees. There's a precise constant in the provided script to describe the angle to the road edge (half of the revolution angle, thus about 60 degrees).

Credits:

[Road sign pictures](https://www.gov.uk/guidance/the-highway-code/traffic-signs)
