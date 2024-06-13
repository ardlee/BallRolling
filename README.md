finalPD.pd

============================================== USAGE ==============================================
To play sounds, turn on DSP in the upper-left corner of finalPD.pd

In finalPD.pd, you will find an embedded PD file: sounds.pd. In this file, you can manually 
update the sounds that are otherwise controlled by unity.

- mastergain: valume of background music
- 'startPlayback' toggle turns on/off the background music
- 'melodyPicker' updates which melody the symth is playing. Click on the message objects labeled
	'MELODIES' to update.
- Use the horizontal slider labelled 'MELODY METRO' to change the speed of the melodies
- Use the sliders labelled 'MANUAL ZPOS' and 'MANUAL XPOS' to manually update those variables
	(changes various synth properties -- see 'the variable section below for more details)
- Click the bang object beneath 'r oscwall' to trigger the collision sound.
	Update fireball and shockwave volume using the labelled sliders
- Each collected sound had a toggle you can click to trigger.
- All of the synth properties can be adjusted using the labelled sliders.


============================================ VARIABLES ============================================
- 'oscTrig' starts bg music playback
- 'points' controls pitch of collect sounds
- 'xPos' controls the following
	> Osc_Gain1
	> LowPass_Gain
	> BandPass_Gain
	> Xpand_Amount
	> Flanger _Feedback
	> Phasor _Feedback
- 'zPos' controls the following
	> Osc_Gain2
	> Osc_Gain4
	> BandPass_Frequency
	> HighPass_Frequency
	> HighPass_Gain
	> Distortion_Amount
	> Distortion _Dry_Wet_
- 'xPos' and 'zPos' together control wind sound
- 'oscwall' triggers collison sound
- 'ballXvelo' and 'ballYvelo' control wind sound
- 'collected', 'collected2', 'collected3', and 'collected4' play unique collect sounds
- 'points' changes the pitch of collect sounds
===================================================================================================


Horizontal resequencing is done as the ball position updates the melody being played.
Vertical resequencing is handled by the 'xPos' and 'zPos' variables.