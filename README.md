HadoukInputSample
=================

A couple sample applications for testing the hadoukinput lib.

This thing has a few different projects:

RawControllerWrapperSample:  This project checks the button down/held/released on the controller and dumps it out to screen.

ControllerWrapperTest: This project checks that the controller wrapper is wrapping up all the controller data correctly before it is going to be sent off to the InputWrapper.  It checks that the "flipped" thing is working correctly to detect forward/back.  Use the 'q' key to change which direction your dude would be facing.  It checks that all the controllers are working.  Use 1-4 keys to swithc controllers.

InputWrapperSample: This verifies that the InputWrapper is working correctly.  First it loads a move list .xml file with a few moves in it. It dumps out the controller buffer, queued input, and most recent action it was able to pull out of the queue.  This one is a good example of how you might use the HadoukInput in a game!

The moves are:

```
A,B,X,Y (z,x,a,s): normal attacks
up + A,B,X,Y (z,x,a,s): high attacks
down + A,B,X,Y (z,x,a,s): low attacks
down, forward, Abutton (z key): hadouken
down, forward, down, forward, Abutton (z key): super hadouken
down, back, Abutton (z key): hurricane kick
```

Cheers!