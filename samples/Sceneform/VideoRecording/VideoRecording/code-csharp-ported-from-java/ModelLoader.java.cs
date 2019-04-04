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
// mc++ package com.google.ar.sceneform.samples.videorecording;
// mc++ 
// mc++ import android.content.Context;
// mc++ import com.google.ar.sceneform.rendering.ModelRenderable;
// mc++ import java.lang.ref.WeakReference;
// mc++ import java.util.concurrent.CompletableFuture;
// mc++ 
// mc++ /**
// mc++  * Model loader class to avoid memory leaks from the activity. Activity and Fragment controller
// mc++  * classes have a lifecycle that is controlled by the UI thread. When a reference to one of these
// mc++  * objects is accessed by a background thread it is "leaked". Using that reference to a
// mc++  * lifecycle-bound object after Android thinks it has "destroyed" it can produce bugs. It also
// mc++  * prevents the Activity or Fragment from being garbage collected, which can leak the memory
// mc++  * permanently if the reference is held in the singleton scope.
// mc++  *
// mc++  * <p>To avoid this, use a non-nested class which is not an activity nor fragment. Hold a weak
// mc++  * reference to the activity or fragment and use that when making calls affecting the UI.
// mc++  */
// mc++ @SuppressWarnings({"AndroidApiChecker"})
// mc++ public class ModelLoader {
// mc++   private static final String TAG = "ModelLoader";
// mc++   private final WeakReference<ModelLoaderCallbacks> owner;
// mc++   private CompletableFuture<ModelRenderable> future;
// mc++ 
// mc++   ModelLoader(ModelLoaderCallbacks owner) {
// mc++     this.owner = new WeakReference<>(owner);
// mc++   }
// mc++ 
// mc++   /**
// mc++    * Starts loading the model specified. The result of the loading is returned asynchrounously via
// mc++    * {@link ModelLoaderCallbacks#setRenderable(ModelRenderable)} or {@link
// mc++    * ModelLoaderCallbacks#onLoadException(Throwable)} (Throwable)}.
// mc++    *
// mc++    * @param resourceId the resource id of the .sfb to load.
// mc++    * @return true if loading was initiated.
// mc++    */
// mc++   boolean loadModel(Context context, int resourceId) {
// mc++ 
// mc++     future =
// mc++         ModelRenderable.builder()
// mc++             .setSource(context, resourceId)
// mc++             .build()
// mc++             .thenApply(this::setRenderable)
// mc++             .exceptionally(this::onException);
// mc++     return future != null;
// mc++   }
// mc++ 
// mc++   ModelRenderable onException(Throwable throwable) {
// mc++     ModelLoaderCallbacks listener = owner.get();
// mc++     if (listener != null) {
// mc++       listener.onLoadException(throwable);
// mc++     }
// mc++     return null;
// mc++   }
// mc++ 
// mc++   ModelRenderable setRenderable(ModelRenderable modelRenderable) {
// mc++     ModelLoaderCallbacks listener = owner.get();
// mc++     if (listener != null) {
// mc++       listener.setRenderable(modelRenderable);
// mc++     }
// mc++     return modelRenderable;
// mc++   }
// mc++ 
// mc++   /** Callbacks for handling the loading results. */
// mc++   public interface ModelLoaderCallbacks {
// mc++     void setRenderable(ModelRenderable modelRenderable);
// mc++ 
// mc++     void onLoadException(Throwable throwable);
// mc++   }
// mc++ }
