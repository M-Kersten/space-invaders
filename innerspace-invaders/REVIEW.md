# Project Review

## Applicant name
Merijn Kersten
---

Overall i'm pretty content with the state of the project.
All the requirements are in there as well as most of the bonus objectives.
The code structure is not as tidy as i would normally make it but given the time constraints i believe it to be sufficient.

What i learned
- Testing should be done with the humble object pattern, i just learned about it so i will research it in my next project when there's more time.
- Serializing of interfaces is not possible in Unity, while i already knew this it came up again and i had to redesign some things. this resulted in the statebehaviour baseclass.

Challenges
- Implementing the game loop took the most time and required the most fixes after implementation.

What i liked or disliked about the test
- Bullet pooling and testing is implemented well i believe.
- I like the visuals but more animations could've been added, on bullet collisions for example.
- In terms of extensibility i would abstract the classes a bit more, especially features like player input and score tracking.
- A factory pattern would work for enemy generation, also i'd do away with the enemygrid object and spawn individual enemies that act more like a boid instead.

- Something i'd do differently in terms of the test itself would be giving the applicant a bit more freedom, by which i mean that the requirements are quite strict given the time limit leaving little space for implementation of novel features.

Documentation
Taskboard for intake test can be found here: https://trello.com/b/UMyZOfWz/innerspace-intake-test
Hourlog can be found here: https://docs.google.com/spreadsheets/d/14tAq0-1h0LmdzOt336o2DlHcNgqcepPMWb1zLTIv-dQ/edit#gid=0

External resources used in project
- background music: https://www.youtube.com/watch?v=mTWWI5LWZ8k
- fonts
	- https://www.dafont.com/outrun-future.font
	- https://www.dafont.com/future-now.font
- leantween: https://assetstore.unity.com/packages/tools/animation/leantween-3595