// mc++ /*
// mc++  * Copyright 2018 Google LLC
// mc++  *
// mc++  * Licensed under the Apache License, Version 2.0 (the "License");
// mc++  * you may not use this file except in compliance with the License.
// mc++  * You may obtain a copy of the License at
// mc++  *
// mc++  *      http://www.apache.org/licenses/LICENSE-2.0
// mc++  *
// mc++  * Unless required by applicable law or agreed to in writing, software
// mc++  * distributed under the License is distributed on an "AS IS" BASIS,
// mc++  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// mc++  * See the License for the specific language governing permissions and
// mc++  * limitations under the License.
// mc++  */
// mc++ package com.google.ar.sceneform.samples.solarsystem;
// mc++ 
// mc++ import android.animation.ObjectAnimator;
// mc++ import android.support.annotation.Nullable;
// mc++ import android.view.animation.LinearInterpolator;
// mc++ import com.google.ar.sceneform.FrameTime;
// mc++ import com.google.ar.sceneform.Node;
// mc++ import com.google.ar.sceneform.math.Quaternion;
// mc++ import com.google.ar.sceneform.math.QuaternionEvaluator;
// mc++ import com.google.ar.sceneform.math.Vector3;
// mc++ 
// mc++ /** Node demonstrating rotation and transformations. */
// mc++ public class RotatingNode extends Node {
// mc++   // We'll use Property Animation to make this node rotate.
// mc++   @Nullable private ObjectAnimator orbitAnimation = null;
// mc++   private float degreesPerSecond = 90.0f;
// mc++ 
// mc++   private final SolarSettings solarSettings;
// mc++   private final boolean isOrbit;
// mc++   private final boolean clockwise;
// mc++   private final float axisTiltDeg;
// mc++   private float lastSpeedMultiplier = 1.0f;
// mc++ 
// mc++   public RotatingNode(
// mc++       SolarSettings solarSettings, boolean isOrbit, boolean clockwise, float axisTiltDeg) {
// mc++     this.solarSettings = solarSettings;
// mc++     this.isOrbit = isOrbit;
// mc++     this.clockwise = clockwise;
// mc++     this.axisTiltDeg = axisTiltDeg;
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onUpdate(FrameTime frameTime) {
// mc++     super.onUpdate(frameTime);
// mc++ 
// mc++     // Animation hasn't been set up.
// mc++     if (orbitAnimation == null) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     // Check if we need to change the speed of rotation.
// mc++     float speedMultiplier = getSpeedMultiplier();
// mc++ 
// mc++     // Nothing has changed. Continue rotating at the same speed.
// mc++     if (lastSpeedMultiplier == speedMultiplier) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     if (speedMultiplier == 0.0f) {
// mc++       orbitAnimation.pause();
// mc++     } else {
// mc++       orbitAnimation.resume();
// mc++ 
// mc++       float animatedFraction = orbitAnimation.getAnimatedFraction();
// mc++       orbitAnimation.setDuration(getAnimationDuration());
// mc++       orbitAnimation.setCurrentFraction(animatedFraction);
// mc++     }
// mc++     lastSpeedMultiplier = speedMultiplier;
// mc++   }
// mc++ 
// mc++   /** Sets rotation speed */
// mc++   public void setDegreesPerSecond(float degreesPerSecond) {
// mc++     this.degreesPerSecond = degreesPerSecond;
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onActivate() {
// mc++     startAnimation();
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onDeactivate() {
// mc++     stopAnimation();
// mc++   }
// mc++ 
// mc++   private long getAnimationDuration() {
// mc++     return (long) (1000 * 360 / (degreesPerSecond * getSpeedMultiplier()));
// mc++   }
// mc++ 
// mc++   private float getSpeedMultiplier() {
// mc++     if (isOrbit) {
// mc++       return solarSettings.getOrbitSpeedMultiplier();
// mc++     } else {
// mc++       return solarSettings.getRotationSpeedMultiplier();
// mc++     }
// mc++   }
// mc++ 
// mc++   private void startAnimation() {
// mc++     if (orbitAnimation != null) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     orbitAnimation = createAnimator(clockwise, axisTiltDeg);
// mc++     orbitAnimation.setTarget(this);
// mc++     orbitAnimation.setDuration(getAnimationDuration());
// mc++     orbitAnimation.start();
// mc++   }
// mc++ 
// mc++   private void stopAnimation() {
// mc++     if (orbitAnimation == null) {
// mc++       return;
// mc++     }
// mc++     orbitAnimation.cancel();
// mc++     orbitAnimation = null;
// mc++   }
// mc++ 
// mc++   /** Returns an ObjectAnimator that makes this node rotate. */
// mc++   private static ObjectAnimator createAnimator(boolean clockwise, float axisTiltDeg) {
// mc++     // Node's setLocalRotation method accepts Quaternions as parameters.
// mc++     // First, set up orientations that will animate a circle.
// mc++     Quaternion[] orientations = new Quaternion[4];
// mc++     // Rotation to apply first, to tilt its axis.
// mc++     Quaternion baseOrientation = Quaternion.axisAngle(new Vector3(1.0f, 0f, 0.0f), axisTiltDeg);
// mc++     for (int i = 0; i < orientations.length; i++) {
// mc++       float angle = i * 360 / (orientations.length - 1);
// mc++       if (clockwise) {
// mc++         angle = 360 - angle;
// mc++       }
// mc++       Quaternion orientation = Quaternion.axisAngle(new Vector3(0.0f, 1.0f, 0.0f), angle);
// mc++       orientations[i] = Quaternion.multiply(baseOrientation, orientation);
// mc++     }
// mc++ 
// mc++     ObjectAnimator orbitAnimation = new ObjectAnimator();
// mc++     // Cast to Object[] to make sure the varargs overload is called.
// mc++     orbitAnimation.setObjectValues((Object[]) orientations);
// mc++ 
// mc++     // Next, give it the localRotation property.
// mc++     orbitAnimation.setPropertyName("localRotation");
// mc++ 
// mc++     // Use Sceneform's QuaternionEvaluator.
// mc++     orbitAnimation.setEvaluator(new QuaternionEvaluator());
// mc++ 
// mc++     //  Allow orbitAnimation to repeat forever
// mc++     orbitAnimation.setRepeatCount(ObjectAnimator.INFINITE);
// mc++     orbitAnimation.setRepeatMode(ObjectAnimator.RESTART);
// mc++     orbitAnimation.setInterpolator(new LinearInterpolator());
// mc++     orbitAnimation.setAutoCancel(true);
// mc++ 
// mc++     return orbitAnimation;
// mc++   }
// mc++ }
