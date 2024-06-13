finalPD.pd

============================ USAGE ============================
'oscTrig' starts bg music playback
'points' controls pitch of collect sounds
'xPos' controls the following.
NOTE: xPos should be within the range [0-1]
	> Osc_Gain1
	> LowPass_Gain
	> BandPass_Gain
	> Xpand_Amount
	> Flanger _Feedback
	> Phasor _Feedback
'zPos' controls the following
NOTE: zPos should be within the range [0-1]
	> Osc_Gain2
	> Osc_Gain4
	> BandPass_Frequency
	> HighPass_Frequency
	> HighPass_Gain
	> Distortion_Amount
	> Distortion _Dry_Wet_
'xPos' and 'zPos' together control wind sound
===============================================================

Horizontal resequencing is done as the ball position updates the melody being played.
Vertical resequencing is handled by the 'xPos' and 'zPos' variables.