# Multi-planetary - Addition to metaverse-space-school

The purpose of this addition is to update the existing functionality of the metaverse-space-school by making it easy to add new locations to visit.

## Background

As a backdrop, the Metaverse Space School was done as a hackathon project early 2023. Metaverse Space School is a VR-powered educational experience that transports students beyond static classroom posters into immersive space exploration. Players take control of Andy, a student who is whisked away to study astronomy firsthand—walking on the Moon, analyzing rocks on Mars, and shaping their own learning journey. Designed to enhance memory retention through visceral experiences, this project is just the beginning of a potential VR education platform, extending interactive learning to subjects like biology, chemistry, and math.

The hackathon entry can be found here: https://hassancortex.itch.io/metaverse-space-school

Details about the project can be found here: https://github.com/lachoneus8/metaverse-space-school/blob/main/README.md

## Multi-planetary

The multi-planetary addition to this project is done by Dave Stevens, one of the original hackathon team members. The goal is to take the existing experience, and add some additional functionality to allow you to transport to many other worlds besides those included in the original experience, which only included Earth, Mars, and the Moon. I aim to make it possible to add many other worlds, and make it easy to do so moving forward.

One of the key values of this hackathon project is that you see first hand the effect of gravity on each of the different worlds. This will be a key part of the multi-planetary experience as well. As you throw and drop items from the VR experience, you will see the results differently, as the gravity will affect the object based on the world you are on.

In order to set up the automation to add new worlds, the following needs to be done:

1. We will need a list of worlds that you can visit. I will set this up as something that can be easily modified from within the Unity Editor. Each world will have the name of the world, and key information about the world including the pull of gravity.
2. Change the way that the world selection works to be dynamic. Selecting which world you travel to is currently hard coded. This will update it to pull from the world list instead.
3. Create a new scene which will be used for all these new worlds. Previous worlds were all custom generated with their own scene. These will remain as they are, but new worlds will use a more generic scene.
4. Update the scene so we can adjust the gravity physics settings based on the data provided in step #1.
5. Update the scene so that it can have a ground plane that looks like you are on the surface of that world. For this, existing pictures of planetary body surfaces will be used in some cases, and in other cases the textures will be generated via AI.
6. Update the view for the table that is part of the scene, so it includes additional information about the planet, including its gravity, average temperature at the equator, its day/night cycle length, its distance from the sun, etc.

## Additional note

The VR space moves really fast. This project was originally built in an earlier version of Unity, with a bunch of tools that were outdated according to latest best practice. Some effort was done in the original branch (https://github.com/lachoneus8/metaverse-space-school/tree/multi-planetary) to bring it up to Unity 6.0.35f1, and update the VR packages used to the latest. This actually took quite a bit of work. This branch (multi-planetary-feature) will just cover the effort from that starting point though. In the original project, there was limited functionality built in to allow for multiple people to join together in the experience, but this has been removed as bringing it up to date would have taken more effort than I could spare at the time.

## AI prompts used

All prompts were fed into Copilot.

The summary under the Background section above was written by AI. I entered in the full summary of the project located here: https://github.com/lachoneus8/metaverse-space-school/blob/main/README.md then made a this promt:
`Could you provide a summary of this project?  Please keep it a single paragraph only a couple sentences long.`

AI was also used to refresh me on MD file markup.

For each world added, I used the following promt to get the information to enter: Could you provide me with the following information about [world name, AKA Marse etc]? Gravity in m/s Minimum nighttime temperature in F at the equator Maximum daytime temperature in F at the equator Day/night cycle length in hours and minutes Distance from the sun in AU

I was having trouble getting good textures to use for planet surfaces, so used this prompt:
Could you help me craft a prompt to create a texture of the surface of Mercury, which will be used for a ground plane in a Unity project? The images I keep getting are of a perspective viewpoint, or show me the whole planet.

Here is the updated prompt I got, which had much better results:
Generate a 2048x2048 seamless texture of the surface of Mercury, viewed directly from above at ground level. The texture should be realistic, showcasing Mercury's rocky, cratered terrain with fine surface details. It should be evenly lit, avoiding harsh shadows or atmospheric effects, and designed to tile seamlessly for use in a Unity ground plane material. Make sure the texture is seamlessly tileable.

## Addendum

With some additional time I went ahead and generated the data for a bunch of other worlds, and a parser to read them into the World scriptable objects. Here are the prompts I used:

Could you make a csv that has the following information? Gravity in m/s2 Min Temperature at the equator in F Max Temperature at the equator in F The rotation length for the body in days, hours, and minutes The distance from the sun in AU The parent body if it is a moon of a planet
I would like rows with this information for each planet in the solar system, each major moon for each planet, Pluto, Charon, and Ceres

This generated the csv file that is contained in Assets/Data/worlds.csv

This data included text for days, months, and hours, so I updated the csv manually to make it easier to parse. It also did not have maximum temperatures for many of the worlds, especially gas giants, since the surface is undefined. I was able to get information for this using this prompt:
Could you provide the best estimates we have for those that are unknown? For gas giants, we can use the temperature at the visible outer gas layer.

This provided good results, which were entered into the CSV.

For parsing the CSV, this prompt was used:
Could you write a script that will allow me to parse this CSV in Unity from an inspector window, with one public variable that has the path for the csv file within the Unity project, and a button to parse it. This script will create serialized Scriptable Objects in a hardcoded folder within the project, using this Scriptable object definition:
[Contents of WorldData script]

At this point, this script has been used to generate the data for all the other planets. It is not hooked up yet into the app yet, as each planet still needs a material, though to fully implement this I could use the results from this site, which has topographical maps pulled from images and data pulled from probe missions: https://stevealbers.net/albers/sos/sos.html
