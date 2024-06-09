synth_BGmusic.pd

============================ USAGE ============================
'oscTrig' starts playback
'points' controls which melody is played. 
	> updates every 9 points
'xPos' controls the following.
NOTE: xPos should be within the range [0-1]
	> Osc_Gain1
	> LowPass_Gain
	> BandPass_Gain
	> Xpand_Amount
	> Flanger _Feedback
	> Phasor _Feedback
'yPos' controls the following
NOTE: zPos should be within the range [0-1]
	> Osc_Gain2
	> Osc_Gain4
	> BandPass_Frequency
	> HighPass_Frequency
	> HighPass_Gain
	> Distortion_Amount
	> Distortion _Dry_Wet_
===============================================================

Horizontal resequencing is done through the 'points' variable as it updates the melody being played.
Vertical resequencing is handled by the 'xPos' and 'zPos' variables.