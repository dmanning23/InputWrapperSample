HadoukInputSample
=================

A couple sample applications for testing the hadoukinput lib.

InputWrapperSample: This verifies that the InputWrapper is working correctly.  First it loads a move list .xml file with a few moves in it. It dumps out the controller buffer, queued input, and most recent action it was able to pull out of the queue.  This one is a good example of how you might use the HadoukInput in a game!

The moves are:

```
A/B/X/Y (z/x/a/s): normal attacks
up + A/B/X/Y (z/x/a/s): high attacks
down + A/B/X/Y (z/x/a/s): low attacks
down, forward, Abutton (z key): hadouken
down, forward, down, forward, Abutton (z key): super hadouken
down, back, Abutton (z key): hurricane kick
```

Currently tested to work on Windows and Ouya.  Other platforms supported by XNA and MonoGame would be easy to implement.

Note: This lib uses a couple of submodules, so make sure to run "git submodule init; git submodule update" after cloning.

Cheers!