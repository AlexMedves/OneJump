#  Hello! And welcome to my project!  
<H6>(Which at the minute is called OneJump, we'll see about the future)</H6>


Currently the game is not realistically in Version 0.00 per se, as I have worked on it for a little bit before adding it to a github repo.
But for the sake of everything on here looking neat, the version numbers will start from 0.00, although you and I both know that is not true :/
(Also to help out, features I added will be put in chronological order of me adding them, this doesn't really help you, as much as it helps me realize where my spaghetti was ruined.)

To keep things interesting, I will also add the playlists or music I listened to while creating a new version for OneJump, just because I thought it would be fun, and who knows, maybe you find something you like.
That list will therefore reside here:

<H4>
[=======Music List=======]</br>
!~Version 0.00: https://www.youtube.com/watch?v=p1dsrd9bJ2c </br>
!~Version 0.01: https://www.youtube.com/watch?v=GU8htjxY6ro </br>
!~Version 0.02: https://www.youtube.com/watch?v=Dvwaq1rwioQ </br>
!~Version 0.03: https://www.youtube.com/watch?v=iTC49Hi4hb8 </br>
!~Version 0.04: https://www.youtube.com/watch?v=tXB7odE1HuA </br>
!~Version 0.05: https://www.youtube.com/watch?v=J0shA9J-4Nc </br>
  
[ |===================| ]
</H4>

<H2>Version 0.00</H2>
<H3>New Features: </H3>
+ Added Planet Mercury.</br>
+ Added General class script for Planets.</br>
+ Added Specific script for Mercury.</br>
+ Adjusted planet Values for Mercury.</br>
+ Added game manager script.</br>
+ Added Venus.</br>
+ Added Camera movement between Mercury and Venus.</br>
+ Added all other planets in solar system (Basic colored Spheres for now).</br>
+ Added Camera movement based on planet index.</br>

<H3>Description</H3>
Overall, I didn't have too much trouble implementing everything so far, fingers crossed it stays that way. The values mentioned above are always subject to change, but I won't bore you with the exact values I use.
If you really want to see the values, you can simply access them within my scripts and/or download the project to see editor specific set values.
That's all really. Thank you for checking out my repo, I appreciate it.

<H2>Version 0.01</H2>
<H3>New Features: </H3>
+ All of the planets now have their own representative color.(Still have to texture them)</br>
+ I changed the game manager script so it doesn't handle the camera as well, giving the actual camera script a purpose. </br>
+ I started working on the timely addition of money.</br>
+ The game now knows what planet you have selected.</br>
+ Started working on Upgrade system scripts.</br>

<H3>Description</H3>
This is where the problems are now starting to rise, specifically because I do not know which script should handle what.
I want the game manager script to be able to handle all of the necessary money values, and work in unison with the upgrade script.
We will see what happens.


<H2>Version 0.02</H2>
<H3>New Features: </H3>
- I have decided to completely ditch the idea of individual scripts, I don't know why I even thought that would be useful. </br>
+ New planet selection system. </br>
+ Added unlocked/locked planets. </br>
+ Each planet now gives you its set amount of money every second. </br>
? Planets may or may not be part of our solar system anymore due to realism, switching to alien planets for now (which can have cooler names anyways). </br>

<H3>Description</H3>
For the next version I should be able to implement some basic upgradability to planets, being able to have upgrades on whichever planet you choose to upgrade and for what resource.
I did finally have my first proper issue, I was trying to figure out the best way of **Genuinely** knowing what planet is selected, and I ended up resorting to a raycast method,
I am simply shooting a timely raycast from the camera position (I say timely, currently the raycast is hit every update, but I will maybe put it down as a coroutine or something so that it has a delay),
facing forward and getting the game object that is currently hit, and storing it as a value.


<H2>Version 0.03</H2>
<H3>New Features: </H3>
- There will now be only three upgrades per planet, with the resource names still to be decided on. </br>
+ Upgrades can now be applied to each resource. </br>
+ Upgrade levels are now shown on each button. </br>
+ You can switch the resource you want to click for. </br>
+ Each resource can now be added per second. </br>

<H3>Description</H3>
This time around I didn't add new features per se, however I developed onto existing ones, adding basic functionality as I will be able to polish them at a later date.
I am unsure of some of the previous code I have written optimization wise that is, but so far it has not caused any issues, I have commented the code so that I can come back
at a later date if needed.

<H2>Version 0.04</H2>
<H3>New Features: </H3>
+ You can now unlock new planets. </br>
+ Upgrades now cost multiple resources. </br>
+ Upgrade values have changed. </br>
+ You can no longer purchase upgrades on planets that are not owned. </br> 
? Started making improvements of workflow. </br>

<H3>Description</H3>
I started having some thoughts about the workflow in some scripts, noticing that I repeat certain operations quite a few times, I want to have function(s)
handle that without chunks of text having to be pretty much copied and pasted across places.
Functions such as this will be placed in GameManager where they can be accessed widely across scripts.

<H2>Version 0.05</H2>
<H3>New Features: </H3>
++ Created working branch separate of main. </br>
+ Planets now have randomly generated names. </br>
+ Mineral/Resource names have been changed. </br>
+ Added Save/Load system. </br>
? Slight easter egg. </br>
+ Started blocking out research/mining facilities. </br>
+ Started working on possible planet models/features. </br>
+ Created one global game timer instead of many instances for each object. </br>
? Changed Gitignore and Gitattributes directory. </br>
+ Fixed slight indexing issue with planet selection. </br>

<H3>Description</H3>
I ran into a little issue when making the Save and Load system, following a tutorial I found online at first, seeing as this was my first ever save/load function, I did my research
and the method that was showcased within that tutorial was not amazing, too resource intensive and overall doing things in a complicated manner. So then I started re-working said system,
instead choosing to go with saving files using JSON, everyone seemed to praise it, with plenty libraries/options to serialize the data. I have not yet serialized said data, but I plan on
using Newtonsoft in order to do that.