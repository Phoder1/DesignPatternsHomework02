# DesignPatternsHomework02
Clone, event and json homework

I used prototype to clone a LaserController every time the laser it controls hits a wall, because of that it helped keep the total life time of all the copies that originated from the same laser the same, disapearing at the same moment.

Also to update the laser's position I added them to a laser manager singleton class which holds an observer which updates them each update.

Also, the main game manger script which holds the laser manager is being saved between scenes with DontDestroyOnLoad()

As for Mediator, I use a laser manager which communicates between all the laser, as well as takes commands from both the player script and the game manager.

For points, you get the total number of copies made from one laser origin when their life ends.
