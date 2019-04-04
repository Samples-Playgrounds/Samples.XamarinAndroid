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
// mc++ import android.content.Context;
// mc++ import android.view.MotionEvent;
// mc++ import android.widget.TextView;
// mc++ import com.google.ar.sceneform.FrameTime;
// mc++ import com.google.ar.sceneform.HitTestResult;
// mc++ import com.google.ar.sceneform.Node;
// mc++ import com.google.ar.sceneform.math.Quaternion;
// mc++ import com.google.ar.sceneform.math.Vector3;
// mc++ import com.google.ar.sceneform.rendering.ModelRenderable;
// mc++ import com.google.ar.sceneform.rendering.ViewRenderable;
// mc++ 
// mc++ /**
// mc++  * Node that represents a planet.
// mc++  *
// mc++  * <p>The planet creates two child nodes when it is activated:
// mc++  *
// mc++  * <ul>
// mc++  *   <li>The visual of the planet, rotates along it's own axis and renders the planet.
// mc++  *   <li>An info card, renders an Android View that displays the name of the planerendt. This can be
// mc++  *       toggled on and off.
// mc++  * </ul>
// mc++  *
// mc++  * The planet is rendered by a child instead of this node so that the spinning of the planet doesn't
// mc++  * make the info card spin as well.
// mc++  */
// mc++ public class Planet extends Node implements Node.OnTapListener {
// mc++   private final String planetName;
// mc++   private final float planetScale;
// mc++   private final float orbitDegreesPerSecond;
// mc++   private final float axisTilt;
// mc++   private final ModelRenderable planetRenderable;
// mc++   private final SolarSettings solarSettings;
// mc++ 
// mc++   private Node infoCard;
// mc++   private RotatingNode planetVisual;
// mc++   private final Context context;
// mc++ 
// mc++   private static final float INFO_CARD_Y_POS_COEFF = 0.55f;
// mc++ 
// mc++   public Planet(
// mc++       Context context,
// mc++       String planetName,
// mc++       float planetScale,
// mc++       float orbitDegreesPerSecond,
// mc++       float axisTilt,
// mc++       ModelRenderable planetRenderable,
// mc++       SolarSettings solarSettings) {
// mc++     this.context = context;
// mc++     this.planetName = planetName;
// mc++     this.planetScale = planetScale;
// mc++     this.orbitDegreesPerSecond = orbitDegreesPerSecond;
// mc++     this.axisTilt = axisTilt;
// mc++     this.planetRenderable = planetRenderable;
// mc++     this.solarSettings = solarSettings;
// mc++     setOnTapListener(this);
// mc++   }
// mc++ 
// mc++   @Override
// mc++   @SuppressWarnings({"AndroidApiChecker", "FutureReturnValueIgnored"})
// mc++   public void onActivate() {
// mc++ 
// mc++     if (getScene() == null) {
// mc++       throw new IllegalStateException("Scene is null!");
// mc++     }
// mc++ 
// mc++     if (infoCard == null) {
// mc++       infoCard = new Node();
// mc++       infoCard.setParent(this);
// mc++       infoCard.setEnabled(false);
// mc++       infoCard.setLocalPosition(new Vector3(0.0f, planetScale * INFO_CARD_Y_POS_COEFF, 0.0f));
// mc++ 
// mc++       ViewRenderable.builder()
// mc++           .setView(context, R.layout.planet_card_view)
// mc++           .build()
// mc++           .thenAccept(
// mc++               (renderable) -> {
// mc++                 infoCard.setRenderable(renderable);
// mc++                 TextView textView = (TextView) renderable.getView();
// mc++                 textView.setText(planetName);
// mc++               })
// mc++           .exceptionally(
// mc++               (throwable) -> {
// mc++                 throw new AssertionError("Could not load plane card view.", throwable);
// mc++               });
// mc++     }
// mc++ 
// mc++     if (planetVisual == null) {
// mc++       // Put a rotator to counter the effects of orbit, and allow the planet orientation to remain
// mc++       // of planets like Uranus (which has high tilt) to keep tilted towards the same direction
// mc++       // wherever it is in its orbit.
// mc++       RotatingNode counterOrbit = new RotatingNode(solarSettings, true, true, 0f);
// mc++       counterOrbit.setDegreesPerSecond(orbitDegreesPerSecond);
// mc++       counterOrbit.setParent(this);
// mc++ 
// mc++       planetVisual = new RotatingNode(solarSettings, false, false, axisTilt);
// mc++       planetVisual.setParent(counterOrbit);
// mc++       planetVisual.setRenderable(planetRenderable);
// mc++       planetVisual.setLocalScale(new Vector3(planetScale, planetScale, planetScale));
// mc++     }
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onTap(HitTestResult hitTestResult, MotionEvent motionEvent) {
// mc++     if (infoCard == null) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     infoCard.setEnabled(!infoCard.isEnabled());
// mc++   }
// mc++ 
// mc++   @Override
// mc++   public void onUpdate(FrameTime frameTime) {
// mc++     if (infoCard == null) {
// mc++       return;
// mc++     }
// mc++ 
// mc++     // Typically, getScene() will never return null because onUpdate() is only called when the node
// mc++     // is in the scene.
// mc++     // However, if onUpdate is called explicitly or if the node is removed from the scene on a
// mc++     // different thread during onUpdate, then getScene may be null.
// mc++     if (getScene() == null) {
// mc++       return;
// mc++     }
// mc++     Vector3 cameraPosition = getScene().getCamera().getWorldPosition();
// mc++     Vector3 cardPosition = infoCard.getWorldPosition();
// mc++     Vector3 direction = Vector3.subtract(cameraPosition, cardPosition);
// mc++     Quaternion lookRotation = Quaternion.lookRotation(direction, Vector3.up());
// mc++     infoCard.setWorldRotation(lookRotation);
// mc++   }
// mc++ }
