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
// mc++ 
// mc++ package com.google.ar.sceneform.samples.animation;
// mc++ 
// mc++ import android.content.res.ColorStateList;
// mc++ import android.os.Bundle;
// mc++ import android.support.design.widget.FloatingActionButton;
// mc++ import android.support.v4.content.ContextCompat;
// mc++ import android.support.v7.app.AppCompatActivity;
// mc++ import android.util.Log;
// mc++ import android.view.Gravity;
// mc++ import android.view.MotionEvent;
// mc++ import android.view.View;
// mc++ import android.widget.Toast;
// mc++ import com.google.ar.core.Anchor;
// mc++ import com.google.ar.core.HitResult;
// mc++ import com.google.ar.core.Plane;
// mc++ import com.google.ar.sceneform.AnchorNode;
// mc++ import com.google.ar.sceneform.FrameTime;
// mc++ import com.google.ar.sceneform.Node;
// mc++ import com.google.ar.sceneform.SkeletonNode;
// mc++ import com.google.ar.sceneform.animation.ModelAnimator;
// mc++ import com.google.ar.sceneform.math.Quaternion;
// mc++ import com.google.ar.sceneform.math.Vector3;
// mc++ import com.google.ar.sceneform.rendering.AnimationData;
// mc++ import com.google.ar.sceneform.rendering.ModelRenderable;
// mc++ import com.google.ar.sceneform.ux.ArFragment;
// mc++ 
// mc++ /** Demonstrates playing animated FBX models. */
// mc++ public class MainActivity extends AppCompatActivity {
// mc++ 
// mc++   private static final String TAG = "AnimationSample";
// mc++   private static final int ANDY_RENDERABLE = 1;
// mc++   private static final int HAT_RENDERABLE = 2;
// mc++   private static final String HAT_BONE_NAME = "hat_point";
// mc++   private ArFragment arFragment;
// mc++   // Model loader class to avoid leaking the activity context.
// mc++   private ModelLoader modelLoader;
// mc++   private ModelRenderable andyRenderable;
// mc++   private AnchorNode anchorNode;
// mc++   private SkeletonNode andy;
// mc++   // Controls animation playback.
// mc++   private ModelAnimator animator;
// mc++   // Index of the current animation playing.
// mc++   private int nextAnimation;
// mc++   // The UI to play next animation.
// mc++   private FloatingActionButton animationButton;
// mc++   // The UI to toggle wearing the hat.
// mc++   private FloatingActionButton hatButton;
// mc++   private Node hatNode;
// mc++   private ModelRenderable hatRenderable;
// mc++ 
// mc++   @Override
// mc++   @SuppressWarnings({"AndroidApiChecker", "FutureReturnValueIgnored"})
// mc++   protected void onCreate(Bundle savedInstanceState) {
// mc++     super.onCreate(savedInstanceState);
// mc++     setContentView(R.layout.activity_main);
// mc++ 
// mc++     arFragment = (ArFragment) getSupportFragmentManager().findFragmentById(R.id.sceneform_fragment);
// mc++ 
// mc++     modelLoader = new ModelLoader(this);
// mc++ 
// mc++     modelLoader.loadModel(ANDY_RENDERABLE, R.raw.andy_dance);
// mc++     modelLoader.loadModel(HAT_RENDERABLE, R.raw.baseball_cap);
// mc++ 
// mc++     // When a plane is tapped, the model is placed on an Anchor node anchored to the plane.
// mc++     arFragment.setOnTapArPlaneListener(this::onPlaneTap);
// mc++ 
// mc++     // Add a frame update listener to the scene to control the state of the buttons.
// mc++     arFragment.getArSceneView().getScene().addOnUpdateListener(this::onFrameUpdate);
// mc++ 
// mc++     // Once the model is placed on a plane, this button plays the animations.
// mc++     animationButton = findViewById(R.id.animate);
// mc++     animationButton.setEnabled(false);
// mc++     animationButton.setOnClickListener(this::onPlayAnimation);
// mc++ 
// mc++     // Place or remove a hat on Andy's head showing how to use Skeleton Nodes.
// mc++     hatButton = findViewById(R.id.hat);
// mc++     hatButton.setEnabled(false);
// mc++     hatButton.setOnClickListener(this::onToggleHat);
// mc++   }
// mc++ 
// mc++   private void onPlayAnimation(View unusedView) {
// mc++     if (animator == null || !animator.isRunning()) {
// mc++       AnimationData data = andyRenderable.getAnimationData(nextAnimation);
// mc++       nextAnimation = (nextAnimation + 1) % andyRenderable.getAnimationDataCount();
// mc++       animator = new ModelAnimator(data, andyRenderable);
// mc++       animator.start();
// mc++       Toast toast = Toast.makeText(this, data.getName(), Toast.LENGTH_SHORT);
// mc++       Log.d(
// mc++           TAG,
// mc++           String.format(
// mc++               "Starting animation %s - %d ms long", data.getName(), data.getDurationMs()));
// mc++       toast.setGravity(Gravity.CENTER, 0, 0);
// mc++       toast.show();
// mc++     }
// mc++   }
// mc++ 
// mc++   /*
// mc++    * Used as the listener for setOnTapArPlaneListener.
// mc++    */
// mc++   private void onPlaneTap(HitResult hitResult, Plane unusedPlane, MotionEvent unusedMotionEvent) {
// mc++     if (andyRenderable == null || hatRenderable == null) {
// mc++       return;
// mc++     }
// mc++     // Create the Anchor.
// mc++     Anchor anchor = hitResult.createAnchor();
// mc++ 
// mc++     if (anchorNode == null) {
// mc++       anchorNode = new AnchorNode(anchor);
// mc++       anchorNode.setParent(arFragment.getArSceneView().getScene());
// mc++ 
// mc++       andy = new SkeletonNode();
// mc++ 
// mc++       andy.setParent(anchorNode);
// mc++       andy.setRenderable(andyRenderable);
// mc++       hatNode = new Node();
// mc++ 
// mc++       // Attach a node to the bone.  This node takes the internal scale of the bone, so any
// mc++       // renderables should be added to child nodes with the world pose reset.
// mc++       // This also allows for tweaking the position relative to the bone.
// mc++       Node boneNode = new Node();
// mc++       boneNode.setParent(andy);
// mc++       andy.setBoneAttachment(HAT_BONE_NAME, boneNode);
// mc++       hatNode.setRenderable(hatRenderable);
// mc++       hatNode.setParent(boneNode);
// mc++       hatNode.setWorldScale(Vector3.one());
// mc++       hatNode.setWorldRotation(Quaternion.identity());
// mc++       Vector3 pos = hatNode.getWorldPosition();
// mc++ 
// mc++       // Lower the hat down over the antennae.
// mc++       pos.y -= .1f;
// mc++ 
// mc++       hatNode.setWorldPosition(pos);
// mc++     }
// mc++   }
// mc++   /**
// mc++    * Called on every frame, control the state of the buttons.
// mc++    *
// mc++    * @param unusedframeTime
// mc++    */
// mc++   private void onFrameUpdate(FrameTime unusedframeTime) {
// mc++     // If the model has not been placed yet, disable the buttons.
// mc++     if (anchorNode == null) {
// mc++       if (animationButton.isEnabled()) {
// mc++         animationButton.setBackgroundTintList(ColorStateList.valueOf(android.graphics.Color.GRAY));
// mc++         animationButton.setEnabled(false);
// mc++         hatButton.setBackgroundTintList(ColorStateList.valueOf(android.graphics.Color.GRAY));
// mc++         hatButton.setEnabled(false);
// mc++       }
// mc++     } else {
// mc++       if (!animationButton.isEnabled()) {
// mc++         animationButton.setBackgroundTintList(
// mc++             ColorStateList.valueOf(ContextCompat.getColor(this, R.color.colorAccent)));
// mc++         animationButton.setEnabled(true);
// mc++         hatButton.setEnabled(true);
// mc++         hatButton.setBackgroundTintList(
// mc++             ColorStateList.valueOf(ContextCompat.getColor(this, R.color.colorPrimary)));
// mc++       }
// mc++     }
// mc++   }
// mc++ 
// mc++   private void onToggleHat(View unused) {
// mc++     if (hatNode != null) {
// mc++       hatNode.setEnabled(!hatNode.isEnabled());
// mc++ 
// mc++       // Set the state of the hat button based on the hat node.
// mc++       if (hatNode.isEnabled()) {
// mc++         hatButton.setBackgroundTintList(
// mc++             ColorStateList.valueOf(ContextCompat.getColor(this, R.color.colorPrimary)));
// mc++       } else {
// mc++         hatButton.setBackgroundTintList(
// mc++             ColorStateList.valueOf(ContextCompat.getColor(this, R.color.colorAccent)));
// mc++       }
// mc++     }
// mc++   }
// mc++ 
// mc++   void setRenderable(int id, ModelRenderable renderable) {
// mc++     if (id == ANDY_RENDERABLE) {
// mc++       this.andyRenderable = renderable;
// mc++     } else {
// mc++       this.hatRenderable = renderable;
// mc++     }
// mc++   }
// mc++ 
// mc++   void onException(int id, Throwable throwable) {
// mc++     Toast toast = Toast.makeText(this, "Unable to load renderable: " + id, Toast.LENGTH_LONG);
// mc++     toast.setGravity(Gravity.CENTER, 0, 0);
// mc++     toast.show();
// mc++     Log.e(TAG, "Unable to load andy renderable", throwable);
// mc++   }
// mc++ }
