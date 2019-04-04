// mc++ package com.google.ar.core.examples.java.helloar;
// mc++ 
// mc++ import com.google.ar.core.Camera;
// mc++ import com.google.ar.core.TrackingFailureReason;
// mc++ 
// mc++ /** Gets human readibly tracking failure reasons and suggested actions. */
// mc++ final class TrackingStateHelper {
// mc++   private static final String INSUFFICIENT_FEATURES_MESSAGE =
// mc++       "Can't find anything. Aim device at a surface with more texture or color.";
// mc++   private static final String EXCESSIVE_MOTION_MESSAGE = "Moving too fast. Slow down.";
// mc++   private static final String INSUFFICIENT_LIGHT_MESSAGE =
// mc++       "Too dark. Try moving to a well-lit area.";
// mc++   private static final String BAD_STATE_MESSAGE =
// mc++       "Tracking lost due to bad internal state. Please try restarting the AR experience.";
// mc++ 
// mc++   public static String getTrackingFailureReasonString(Camera camera) {
// mc++     TrackingFailureReason reason = camera.getTrackingFailureReason();
// mc++     switch (reason) {
// mc++       case NONE:
// mc++         return "";
// mc++       case BAD_STATE:
// mc++         return BAD_STATE_MESSAGE;
// mc++       case INSUFFICIENT_LIGHT:
// mc++         return INSUFFICIENT_LIGHT_MESSAGE;
// mc++       case EXCESSIVE_MOTION:
// mc++         return EXCESSIVE_MOTION_MESSAGE;
// mc++       case INSUFFICIENT_FEATURES:
// mc++         return INSUFFICIENT_FEATURES_MESSAGE;
// mc++     }
// mc++     return "Unknown tracking failure reason: " + reason;
// mc++   }
// mc++ }
